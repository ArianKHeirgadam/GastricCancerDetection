using System.ComponentModel.DataAnnotations.Schema;
namespace GastricCancerDetection.Domain.Entities;
[Table("EarlyStageScores", Schema="analysis")]
public class EarlyStageScore { public long Id {get;set;} public long CandidateId {get;set;} public int AnalysisRunId {get;set;} public double StageI {get;set;} public double StageII {get;set;} public double Precursor {get;set;} public double LowTumorBurden {get;set;} public double CfDNARelevance {get;set;} public double Score {get;set;} public DateTime CreatedAt {get;set;} }
