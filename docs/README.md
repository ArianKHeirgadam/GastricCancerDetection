# GastricCancerDetection — GRCh38 annotation phase v2

This package contains the current Python annotation phase only.

## What is implemented

1. Parses VCF input.
2. Preserves GRCh38 as the configured reference build.
3. Supports offline classification without inventing annotations.
4. Calls the official Ensembl VEP REST API for real annotation.
5. Calls Ensembl `info/data` to capture the data release when the server reports it.
6. Preserves transcript consequences and regulatory-feature consequences.
7. Keeps promoter/enhancer classification conservative: a generic
   `regulatory_region_variant` is reported as `regulatory_region` and is
   NOT silently converted into promoter or enhancer.
8. Keeps ClinVar, dbSNP and population-frequency fields null unless the
   returned VEP data actually contains corresponding information.
9. Records annotation status and errors for provenance/auditability.

## Official API basis

The VEP implementation uses the Ensembl REST region endpoint for multi-variant
annotation and the Ensembl `info/data` endpoint for the server's reported data
release.

The project does not claim that VEP alone replaces direct ClinVar, dbSNP,
gnomAD, ENCODE, FANTOM5, GENCODE or UCSC source integration. Those remain
separate source integrations for later phases.

## Run offline

From the project root:

```powershell
& C:\Users\Arian\AppData\Local\Python\pythoncore-3.14-64\python.exe `
  .\PythonScripts\main.py `
  --vcf ".\src\GastricCancerDetection.Api\storage\genomes\781893e9-c530-4bc6-b749-8b3f0271e5a3.vcf" `
  --offline `
  --output ".\annotation-offline-test.json"
```

Expected properties:

- `status`: `completed`
- `variant_count`: `1`
- `annotation_status`: `not_annotated`
- `region`: `unknown`
- `is_noncoding`: `false`

## Run real Ensembl VEP

```powershell
& C:\Users\Arian\AppData\Local\Python\pythoncore-3.14-64\python.exe `
  .\PythonScripts\main.py `
  --vcf ".\src\GastricCancerDetection.Api\storage\genomes\781893e9-c530-4bc6-b749-8b3f0271e5a3.vcf" `
  --output ".\annotation-vep-test.json"
```

Internet access to `rest.ensembl.org` is required.

## Run the local smoke test

```powershell
& C:\Users\Arian\AppData\Local\Python\pythoncore-3.14-64\python.exe `
  .\tests\smoke_test.py
```

## Scientific caution

The package deliberately does not infer that an unclassified or generic
regulatory variant is a promoter/enhancer hit. A regulatory feature ID and
`regulatory_region_variant` consequence are retained as evidence, but exact
functional class requires an appropriate regulatory source/feature mapping.

This package is not a clinical diagnostic system and does not establish
clinical validity or diagnostic performance.
