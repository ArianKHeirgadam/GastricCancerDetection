namespace GastricCancerDetection.Application.Interfaces; public interface ICandidateScoringService { Task RankAsync(int runId,CancellationToken ct=default); }
