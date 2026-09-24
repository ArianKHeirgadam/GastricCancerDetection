# GastricCancerDetection — .NET 8 + SQL Server + Python

This repository is an incremental Phase 1 upgrade of the existing GastricCancerDetection system. The baseline gene-panel/Fisher/risk workflow remains intact. No EF migrations are introduced; the database remains Database-First.

## Phase 1 implemented
- Non-coding hotspot data contract and API surface
- Arm-level/focal CNA data contract and API surface
- Candidate/evidence/ranking model
- cfDNA detectability engine with explicit sub-scores
- in-silico dilution simulation module
- versioned model metadata
- AnalysisRun reproducibility fields
- additive Excel workbook schema
- future mtDNA/PMD/repeat-methylation/multi-omics tables without fake analysis records

## Baseline preserved
Subject creation, Sample creation, genome file upload, baseline AnalysisRun, Fisher/risk result entities, and existing report download endpoints are preserved.

## Database
Run `Database/02_phase1_additive_schema.sql` manually against `gastriccancerdb`. This is an additive Database-First script. Do not create EF migrations for this phase.

## Python
Python modules are under `PythonScripts/`. Install with `python -m pip install -r PythonScripts/requirements.txt`. The Python pipeline emits JSON and uses the labels `in-silico estimate` for computational detection simulations.

The advanced pipeline is disabled by default. Enable it only after the Phase 1 database schema is applied and real input data/contracts are populated:
`Analysis:EnableAdvancedPipeline = true`

## Scientific integrity
No clinical detection limit, AUC, sensitivity, specificity, cohort size, validation count, or effect size is fabricated. Missing data must remain NA/Not reported/Not located in searched sources. mtDNA, PMD, LINE-1/Alu, and integrated multi-omics are architecture-only until real data contracts are supplied.

## Security
Do not commit real connection strings, JWT secrets, API keys, tokens, patient-identifiable data, VCF/BAM/CRAM files, or generated reports. Use User Secrets/environment variables for local secrets. `.gitignore.txt` was removed; `.gitignore` is the only ignore file.

## New API
- `POST /api/analysis/run`
- `GET /api/analysis/runs/{runId}`
- `GET /api/analysis/ranking/{runId}`
- `GET /api/candidates`
- `GET /api/candidates/{id}`
- `GET /api/candidates/{id}/evidence`
- `GET /api/candidates/{id}/cfDNA-score`
- `GET /api/cnv`
- `GET /api/noncoding-hotspots`
- `POST /api/reports/generate/{runId}?format=Excel`

## Scoring model
Default configurable weights:
- Biological Evidence 20%
- Statistical Robustness 15%
- Literature Whitespace 15%
- Validation Gap 15%
- Early-stage Relevance 15%
- GC Specificity 10%
- cfDNA Detectability 10%

The weights are implementation defaults, not clinical validation.
