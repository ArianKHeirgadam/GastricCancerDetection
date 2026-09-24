from dataclasses import dataclass


@dataclass
class Variant:
    chrom: str
    pos: int
    ref: str
    alt: str
    vaf: float | None = None
    info: dict | None = None


def parse_vcf(path):
    with open(path, "rt", encoding="utf-8", errors="replace") as f:
        for line in f:
            if line.startswith("#") or not line.strip():
                continue

            parts = line.rstrip("\n\r").split("\t")
            if len(parts) < 5:
                continue

            info = {}
            if len(parts) > 7 and parts[7] != ".":
                for item in parts[7].split(";"):
                    key_value = item.split("=", 1)
                    info[key_value[0]] = (
                        key_value[1] if len(key_value) == 2 else True
                    )

            vaf = None
            for key in ("VAF", "AF"):
                if key in info:
                    try:
                        vaf = float(str(info[key]).split(",")[0])
                    except ValueError:
                        pass
                    break

            yield Variant(
                chrom=parts[0],
                pos=int(parts[1]),
                ref=parts[3],
                alt=parts[4],
                vaf=vaf,
                info=info,
            )
