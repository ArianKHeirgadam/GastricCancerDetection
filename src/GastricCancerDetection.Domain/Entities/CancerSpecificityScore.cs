using System.ComponentModel.DataAnnotations.Schema;
namespace GastricCancerDetection.Domain.Entities;
[Table("CancerSpecificityScores", Schema="analysis")]
public class CancerSpecificityScore { public long Id {get;set;} public long CandidateId {get;set;} public int AnalysisRunId {get;set;} public double Healthy {get;set;} public double BenignGastric {get;set;} public double OtherCancer {get;set;} public double Score {get;set;} public DateTime CreatedAt {get;set;} }
