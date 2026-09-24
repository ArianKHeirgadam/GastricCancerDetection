namespace GastricCancerDetection.Application.Interfaces; public interface IGenomeAnalysisService { Task<object> RunAdvancedPipelineAsync(int runId,CancellationToken ct=default); }
