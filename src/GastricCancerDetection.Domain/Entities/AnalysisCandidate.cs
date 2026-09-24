using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GastricCancerDetection.Domain.Entities;

[Table("Candidates", Schema = "analysis")]
public class AnalysisCandidate
{
    [Key]
    public long CandidateId { get; set; }

    public int AnalysisRunId { get; set; }

    public int? ModelVersionId { get; set; }

    public string CandidateKey { get; set; } = null!;

    public string FeatureType { get; set; } = null!;

    public string? CandidateName { get; set; }

    public string? Chromosome { get; set; }

    public long? Start { get; set; }

    public long? End { get; set; }

    public long? RegionSizeBp { get; set; }

    public string? TumorEffect { get; set; }

    public double? StatisticalRobustness { get; set; }

    public double? BiologicalEvidence { get; set; }

    public double? LiteratureWhitespace { get; set; }

    public double? ValidationGap { get; set; }

    public double? EarlyStageScore { get; set; }

    public double? CancerSpecificityScore { get; set; }

    public double? CfDNADetectabilityScore { get; set; }

    public double? OverallPriority { get; set; }

    public string EvidenceStatus { get; set; } = "Insufficient Evidence";

    public DateTime CreatedAt { get; set; }
}