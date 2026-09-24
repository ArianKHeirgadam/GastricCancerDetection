namespace GastricCancerDetection.Application.Interfaces; public interface IVariantAnalysisService { Task<int> AnalyzeNonCodingAsync(int runId,CancellationToken ct=default); }
