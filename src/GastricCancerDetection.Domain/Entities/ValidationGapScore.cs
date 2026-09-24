using System.ComponentModel.DataAnnotations.Schema;
namespace GastricCancerDetection.Domain.Entities;
[Table("ValidationGapScores", Schema="analysis")]
public class ValidationGapScore { public long Id {get;set;} public long CandidateId {get;set;} public int AnalysisRunId {get;set;} public double Score {get;set;} public DateTime CreatedAt {get;set;} }
