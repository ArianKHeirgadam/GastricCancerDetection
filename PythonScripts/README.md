# GastricCancerDetection — GRCh38 Annotation Phase

This package implements the next incremental phase of the existing Python pipeline:

VCF -> GRCh38 Ensembl VEP annotation -> provenance -> conservative noncoding classification -> JSON

## Files

- `main.py` — pipeline entry point.
- `vcf_parser.py` — existing VCF parser, preserved.
- `noncoding_hotspot.py` — conservative classifier with provenance fields.
- `annotation_provider.py` — real Ensembl VEP REST integration.
- `config.py` — pipeline metadata plus annotation source configuration.

## Annotation policy

- GRCh38 is the configured genome build.
- Ensembl VEP is used as the primary annotation source.
- Missing data is represented as `null` and is never treated as negative evidence.
- Annotation status is one of `annotated`, `not_annotated`, `partial`, or `error`.
- An intronic consequence is reported as `intronic`; it is NOT automatically promoted to `regulatory_intronic`.
- ClinVar, dbSNP and population-frequency values are not fabricated. They are populated only when the VEP response actually exposes corresponding colocated data.
- A future dedicated ClinVar/dbSNP/gnomAD integration should retain its own source and version.

## Commands

Online annotation:

```powershell
python .\PythonScripts\main.py --vcf .\path\to\input.vcf --output .\annotation-result.json
```

Offline structural test:

```powershell
python .\PythonScripts\main.py --vcf .\path\to\input.vcf --offline --output .\annotation-result.json
```

The offline mode is only for pipeline tests. It does not produce scientific annotation.

## External dependency

No third-party Python package is required. The provider uses Python's standard-library `urllib`.

The online path requires network access to the public Ensembl REST service.
