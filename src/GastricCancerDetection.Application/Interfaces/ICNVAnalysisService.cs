namespace GastricCancerDetection.Application.Interfaces; public interface ICNVAnalysisService { Task<int> AnalyzeAsync(int runId,CancellationToken ct=default); }
