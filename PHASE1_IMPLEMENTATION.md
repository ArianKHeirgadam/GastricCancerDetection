# Phase 1 Implementation Notes

## Scope
Implemented additively on the existing four-project .NET 8 solution:
- non-coding hotspot data contract/API
- CNA data contract/API
- candidate/evidence model
- cfDNA detectability scoring and in-silico dilution simulator
- reproducible AnalysisRun metadata
- model version registry
- Excel Phase 1 workbook skeleton
- future mtDNA/PMD/repeat methylation/integrated panel schema without fake records

## Important limitation
The supplied project snapshot did not contain the original PythonScripts/SampleData folders, and the execution environment does not have the .NET SDK or SQL Server. Therefore the package includes the Phase 1 code, SQL schema, Python pipeline modules, and smoke tests, but it has not been runtime-validated against the user's SQL Server instance. The advanced pipeline remains disabled by default.

## Activation
1. Apply `Database/02_phase1_additive_schema.sql` to `gastriccancerdb`.
2. Install Python dependencies from `PythonScripts/requirements.txt`.
3. Verify the existing baseline endpoints first.
4. Populate real annotation/CNA data contracts; do not create synthetic scientific records.
5. Set `Analysis:EnableAdvancedPipeline` to `true` only after those prerequisites are met.
