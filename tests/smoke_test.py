import json
import sys
from pathlib import Path

ROOT = Path(__file__).resolve().parents[1]
sys.path.insert(0, str(ROOT / "PythonScripts"))

from config import PipelineConfig
from noncoding_hotspot import classify_variant
from vcf_parser import parse_vcf


def main():
    vcf = ROOT / "test.vcf"
    variants = list(parse_vcf(str(vcf)))

    assert len(variants) == 1
    assert variants[0].chrom == "chr17"
    assert variants[0].pos == 7579472
    assert variants[0].ref == "C"
    assert variants[0].alt == "T"
    assert variants[0].vaf == 0.5

    offline = classify_variant(variants[0])

    assert offline["region"] == "unknown"
    assert offline["is_noncoding"] is False
    assert offline["annotation_status"] == "not_annotated"

    annotated = classify_variant(
        variants[0],
        {
            "gene_symbol": "CD68",
            "transcript_consequences": [
                {
                    "transcript_id": "ENST00000250092",
                    "gene_symbol": "CD68",
                    "consequence_terms": ["upstream_gene_variant"],
                }
            ],
            "regulatory_feature_consequences": [
                {
                    "regulatory_feature_id": "ENSR17_B3HLJ",
                    "consequence_terms": ["regulatory_region_variant"],
                }
            ],
            "annotation_source": "Ensembl VEP REST",
            "annotation_version": "Ensembl data release TEST",
            "annotation_status": "annotated",
        },
    )

    assert annotated["gene"] == "CD68"
    assert annotated["transcript"] == "ENST00000250092"
    assert annotated["region"] == "regulatory_region"
    assert annotated["is_noncoding"] is False
    assert annotated["annotation_version"] == "Ensembl data release TEST"

    print("SMOKE TEST PASSED")
    print(json.dumps({
        "pipeline_version": PipelineConfig.pipeline_version,
        "genome_build": PipelineConfig.genome_build,
        "variant_count": len(variants),
        "offline_region": offline["region"],
        "annotated_gene": annotated["gene"],
        "annotated_regulatory_feature": (
            annotated["regulatory_annotation"][0]["id"]
        ),
    }, indent=2))


if __name__ == "__main__":
    main()
