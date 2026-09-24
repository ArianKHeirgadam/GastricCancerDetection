namespace GastricCancerDetection.Application.Interfaces; public interface ICfDNADetectabilityService { Task CalculateAsync(int runId,CancellationToken ct=default); }
