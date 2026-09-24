using GastricCancerDetection.Application.DTOs;
namespace GastricCancerDetection.Application.Interfaces;
public interface IAnalysisService { Task<object> StartAsync(int panelId,int modelVersionId,CancellationToken ct=default); Task<object> StartAdvancedAsync(AnalysisRunRequestDto request,CancellationToken ct=default); }
