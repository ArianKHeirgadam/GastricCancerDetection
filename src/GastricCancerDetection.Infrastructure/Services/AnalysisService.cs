using Microsoft.Extensions.Configuration;
using GastricCancerDetection.Application.Interfaces;
using GastricCancerDetection.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace GastricCancerDetection.Infrastructure.Services;

public class AnalysisService : IAnalysisService
{
    private readonly GastricCancerDbContext _db;
    private readonly IConfiguration _config;
    private readonly IGenomeAnalysisService _advanced;
    public AnalysisService(GastricCancerDbContext db, IConfiguration config, IGenomeAnalysisService advanced){_db=db;_config=config;_advanced=advanced;}

    public async Task<object> StartAdvancedAsync(GastricCancerDetection.Application.DTOs.AnalysisRunRequestDto request, CancellationToken ct = default)
    {
        return await StartAsync(request.PanelId, request.ModelVersionId, ct);
    }

    public async Task<object> StartAsync(int panelId, int modelVersionId, CancellationToken ct = default)
    {
        var panel = await _db.GenePanels.FindAsync(new object[]{panelId}, ct);
        var model = await _db.RiskModelVersions.FindAsync(new object[]{modelVersionId}, ct);
        if(panel is null || model is null) throw new InvalidOperationException("Panel or risk model not found.");
        if(!model.IsApprovedForUse) throw new InvalidOperationException("The selected risk model is not approved for use.");

        var run = new Domain.Entities.AnalysisRun {
            PanelId=panelId, ModelVersionId=modelVersionId, RunDate=DateTime.UtcNow,
            HealthyCount=await _db.Subjects.CountAsync(x=>x.IsHealthy && !x.IsDeleted,ct),
            UnhealthyCount=await _db.Subjects.CountAsync(x=>!x.IsHealthy && !x.IsDeleted,ct),
            StatusId=1, PipelineVersion=_config["Analysis:PipelineVersion"] ?? "1.0", Dataset="Current repository dataset", GenomeBuild="GRCh38", PythonVersion=_config["Analysis:PythonVersion"] ?? "3.x", StartTime=DateTime.UtcNow
        };
        _db.AnalysisRuns.Add(run);
        await _db.SaveChangesAsync(ct);

        if(string.Equals(_config["Analysis:EnableAdvancedPipeline"],"true",StringComparison.OrdinalIgnoreCase)) return await _advanced.RunAdvancedPipelineAsync(run.RunId,ct);
        return new { run.RunId, run.PanelId, run.ModelVersionId, run.HealthyCount, run.UnhealthyCount, Status= "Pending" };
    }
}
