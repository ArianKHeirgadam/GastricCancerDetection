using System.ComponentModel.DataAnnotations.Schema;
namespace GastricCancerDetection.Domain.Entities;
[Table("CandidateEvidence", Schema="analysis")]
public class CandidateEvidence {
 public long EvidenceId { get; set; }
 public long CandidateId { get; set; }
 public int AnalysisRunId { get; set; }
 public string EvidenceType { get; set; } = null!;
 public string? Source { get; set; }
 public string? SourceIdentifier { get; set; }
 public string? EvidenceLevel { get; set; }
 public double? NumericValue { get; set; }
 public string? ValueText { get; set; }
 public string? Notes { get; set; }
 public DateTime CreatedAt { get; set; }
}
