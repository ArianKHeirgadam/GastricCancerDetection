using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GastricCancerDetection.Domain.Entities;

[Table("AnalysisRuns", Schema = "analysis")]
public class AnalysisRun
{
    [Key]
    public int RunId { get; set; }

    public int PanelId { get; set; }

    public int ModelVersionId { get; set; }

    public DateTime RunDate { get; set; }

    public int? TriggeredByUserId { get; set; }

    public string? PipelineVersion { get; set; }

    public int HealthyCount { get; set; }

    public int UnhealthyCount { get; set; }

    public string? Notes { get; set; }

    public byte StatusId { get; set; }

    public DateTime ValidFrom { get; set; }

    public DateTime ValidTo { get; set; }

    public string? Dataset { get; set; }

    public string? GenomeBuild { get; set; }

    public string? PythonVersion { get; set; }

    public string? Parameters { get; set; }

    public DateTime? StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    public string? ErrorMessage { get; set; }
}