from dataclasses import dataclass


@dataclass(frozen=True)
class PipelineConfig:
    genome_build: str = "GRCh38"
    pipeline_version: str = "2.0-phase1"
    model_version: str = "cfDNA-in-silico-v1"
    dilution_fractions: tuple = (0.50, 0.20, 0.10, 0.05, 0.02, 0.01, 0.005)

    annotation_source: str = "Ensembl VEP REST"
    annotation_species: str = "homo_sapiens"
    annotation_data_endpoint: str = "https://rest.ensembl.org/info/data"
    annotation_vep_endpoint: str = "https://rest.ensembl.org/vep/homo_sapiens/region"
    annotation_timeout_seconds: int = 30
