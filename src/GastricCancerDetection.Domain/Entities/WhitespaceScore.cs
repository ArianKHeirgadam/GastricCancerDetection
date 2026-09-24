using System.ComponentModel.DataAnnotations.Schema;
namespace GastricCancerDetection.Domain.Entities;
[Table("WhitespaceScores", Schema="analysis")]
public class WhitespaceScore { public long Id {get;set;} public long CandidateId {get;set;} public int AnalysisRunId {get;set;} public double SaturationIndex {get;set;} public double WhitespaceIndex {get;set;} public string Classification {get;set;}="Insufficient Evidence"; public DateTime CreatedAt {get;set;} }
