import argparse
import json
import sys
from pathlib import Path

from annotation_provider import EnsemblVepAnnotationProvider
from config import PipelineConfig
from noncoding_hotspot import classify_variant
from vcf_parser import parse_vcf


def _write_json(text, output):
    if not output:
        return

    output_path = Path(output)
    output_path.parent.mkdir(parents=True, exist_ok=True)
    output_path.write_text(text, encoding="utf-8")


def main():
    parser = argparse.ArgumentParser()
    parser.add_argument("--vcf", required=True)
    parser.add_argument("--output", required=False)
    parser.add_argument("--offline", action="store_true")

    args = parser.parse_args()
    vcf_path = Path(args.vcf)

    result = {
        "pipeline_version": PipelineConfig.pipeline_version,
        "genome_build": PipelineConfig.genome_build,
        "status": "started",
        "input_file": str(vcf_path.resolve()),
        "variant_count": 0,
        "noncoding_variants": [],
        "error": None,
    }

    if not vcf_path.exists():
        result["status"] = "failed"
        result["error"] = "VCF file not found."

        text = json.dumps(
            result, separators=(",", ":"), ensure_ascii=False
        )
        _write_json(text, args.output)
        print(text, file=sys.stderr)
        return 1

    try:
        variants = list(parse_vcf(str(vcf_path)))
        result["variant_count"] = len(variants)

        provider = None if args.offline else EnsemblVepAnnotationProvider()

        classified = []
        for variant in variants:
            annotation = None
            if provider is not None:
                annotation = provider.annotate_variant(variant)

            classified.append(
                classify_variant(
                    variant,
                    annotation=annotation,
                )
            )

        result["noncoding_variants"] = classified
        result["status"] = "completed"

    except Exception as exc:
        result["status"] = "failed"
        result["error"] = str(exc)

        text = json.dumps(
            result, separators=(",", ":"), ensure_ascii=False
        )
        _write_json(text, args.output)
        print(text, file=sys.stderr)
        return 1

    text = json.dumps(
        result, separators=(",", ":"), ensure_ascii=False
    )
    _write_json(text, args.output)
    print(text)
    return 0


if __name__ == "__main__":
    sys.exit(main())
