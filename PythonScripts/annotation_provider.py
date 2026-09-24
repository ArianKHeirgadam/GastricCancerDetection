import json
from urllib.error import HTTPError, URLError
from urllib.request import Request, urlopen

from config import PipelineConfig


class EnsemblVepAnnotationProvider:
    def __init__(
        self,
        timeout_seconds: int | None = None,
        vep_endpoint: str | None = None,
        data_endpoint: str | None = None,
    ):
        self.timeout_seconds = (
            timeout_seconds or PipelineConfig.annotation_timeout_seconds
        )
        self.vep_endpoint = (
            vep_endpoint or PipelineConfig.annotation_vep_endpoint
        )
        self.data_endpoint = (
            data_endpoint or PipelineConfig.annotation_data_endpoint
        )

    def _request_json(self, request):
        with urlopen(request, timeout=self.timeout_seconds) as response:
            payload = response.read().decode("utf-8")
            return json.loads(payload)

    def _get_data_release(self):
        request = Request(
            self.data_endpoint,
            headers={
                "Accept": "application/json",
                "Content-Type": "application/json",
                "User-Agent": "GastricCancerDetection/2.0",
            },
            method="GET",
        )

        payload = self._request_json(request)

        releases = []

        if isinstance(payload, dict):
            raw = payload.get("releases")

            if isinstance(raw, list):
                releases = raw
            elif isinstance(raw, str):
                releases = [raw]
            elif "release" in payload:
                releases = [payload["release"]]

        if not releases:
            return None

        return str(releases[0])

    @staticmethod
    def _first_non_empty(*values):
        for value in values:
            if value not in (None, "", [], {}):
                return value

        return None

    @staticmethod
    def _extract_population_frequency(result):
        for key in ("colocated_variants", "colocated"):
            colocated = result.get(key) or []

            if not isinstance(colocated, list):
                continue

            for variant in colocated:
                if not isinstance(variant, dict):
                    continue

                for frequency_key in (
                    "frequencies",
                    "frequency",
                    "allele_freq",
                ):
                    value = variant.get(frequency_key)

                    if value not in (None, "", {}, []):
                        return value

        return None

    @staticmethod
    def _extract_clinvar(result):
        colocated = result.get("colocated_variants") or []

        if not isinstance(colocated, list):
            return None

        evidence = []

        for variant in colocated:
            if not isinstance(variant, dict):
                continue

            identifiers = variant.get("clin_sig")

            if identifiers:
                evidence.append(identifiers)

        return evidence or None

    @staticmethod
    def _extract_dbsnp(result):
        colocated = result.get("colocated_variants") or []

        if not isinstance(colocated, list):
            return None

        identifiers = []

        for variant in colocated:
            if not isinstance(variant, dict):
                continue

            identifier = variant.get("id")

            if (
                isinstance(identifier, str)
                and identifier.startswith("rs")
            ):
                identifiers.append(identifier)

        return sorted(set(identifiers)) or None

    @staticmethod
    def _build_vep_variant(variant):
        """
        Build the VCF-like variant representation expected by
        Ensembl VEP POST /vep/{species}/region.

        Format:
            CHROM POS ID REF ALT QUAL FILTER INFO
        """

        return (
            f"{variant.chrom} "
            f"{variant.pos} "
            f". "
            f"{variant.ref} "
            f"{variant.alt} "
            f". "
            f". "
            f"."
        )

    def annotate_variant(self, variant):
        release = None

        try:
            release = self._get_data_release()

            variant_string = self._build_vep_variant(variant)

            body = json.dumps(
                {
                    "variants": [variant_string]
                }
            ).encode("utf-8")

            request = Request(
                self.vep_endpoint,
                data=body,
                headers={
                    "Accept": "application/json",
                    "Content-Type": "application/json",
                    "User-Agent": "GastricCancerDetection/2.0",
                },
                method="POST",
            )

            payload = self._request_json(request)

            if not isinstance(payload, list) or not payload:
                raise ValueError(
                    "Ensembl VEP returned an empty or unexpected response."
                )

            result = payload[0]

            if not isinstance(result, dict):
                raise ValueError(
                    "Ensembl VEP returned an invalid result."
                )

            if release:
                version = f"Ensembl data release {release}"
            else:
                version = (
                    "Ensembl VEP REST "
                    "(data release not reported)"
                )

            return {
                **result,
                "annotation_source": "Ensembl VEP REST",
                "annotation_version": version,
                "annotation_status": "annotated",
                "annotation_error": None,
                "population_frequency": (
                    self._extract_population_frequency(result)
                ),
                "clinvar_evidence": (
                    self._extract_clinvar(result)
                ),
                "dbsnp_identifier": (
                    self._extract_dbsnp(result)
                ),
            }

        except (
            HTTPError,
            URLError,
            TimeoutError,
            ValueError,
            json.JSONDecodeError,
        ) as exc:
            version = (
                f"Ensembl data release {release}"
                if release
                else (
                    "Ensembl VEP REST "
                    "(data release unavailable)"
                )
            )

            return {
                "annotation_source": "Ensembl VEP REST",
                "annotation_version": version,
                "annotation_status": "failed",
                "annotation_error": str(exc),
            }

        except Exception as exc:
            return {
                "annotation_source": "Ensembl VEP REST",
                "annotation_version": (
                    f"Ensembl data release {release}"
                    if release
                    else (
                        "Ensembl VEP REST "
                        "(data release unavailable)"
                    )
                ),
                "annotation_status": "failed",
                "annotation_error": str(exc),
            }