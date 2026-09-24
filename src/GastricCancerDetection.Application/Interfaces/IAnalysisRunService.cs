namespace GastricCancerDetection.Application.Interfaces; public interface IAnalysisRunService { Task<object?> GetAsync(int runId,CancellationToken ct=default); }
