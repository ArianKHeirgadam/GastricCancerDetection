using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace GastricCancerDetection.Domain.Entities;

[Table("SubjectRiskResults", Schema="analysis")]
public class SubjectRiskResult { public long ResultId {get;set;} public int RunId {get;set;} public int SubjectId {get;set;} public int? SampleId {get;set;} public double TotalRiskScore {get;set;} public byte RiskCategoryId {get;set;} public string? ContributingGenes {get;set;} public DateTime GeneratedAt {get;set;} }

