using System.ComponentModel.DataAnnotations.Schema;
namespace GastricCancerDetection.Domain.Entities;
[Table("LiteratureEvidence", Schema="analysis")]
public class LiteratureEvidence {
 public long LiteratureEvidenceId { get; set; }
 public long CandidateId { get; set; }
 public int AnalysisRunId { get; set; }
 public int PaperCount { get; set; }
 public int StudyCount { get; set; }
 public int CohortCount { get; set; }
 public int IndependentCohortCount { get; set; }
 public int ExternalValidationCount { get; set; }
 public int MulticenterCount { get; set; }
 public int StageICount { get; set; }
 public int StageIICount { get; set; }
 public int BenignControlCount { get; set; }
 public int OtherCancerControlCount { get; set; }
 public int AssayCount { get; set; }
 public int? FirstYear { get; set; }
 public int? LatestYear { get; set; }
 public double? LiteratureSaturationIndex { get; set; }
 public double? LiteratureWhitespaceIndex { get; set; }
 public double? ValidationGapScore { get; set; }
 public string WhitespaceClass { get; set; } = "Insufficient Evidence";
 public string? EvidenceNotes { get; set; }
 public DateTime CreatedAt { get; set; }
}
