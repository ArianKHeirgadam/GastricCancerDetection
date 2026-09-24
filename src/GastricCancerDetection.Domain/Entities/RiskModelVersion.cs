using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace GastricCancerDetection.Domain.Entities;

[Table("RiskModelVersions", Schema="analysis")]
public class RiskModelVersion { public int ModelVersionId {get;set;} public string VersionName {get;set;}=null!; public string? Description {get;set;} public string StatisticalMethod {get;set;}="Fisher_Exact_Test"; public string? ScoringFormula {get;set;} public int? ValidatedByUserId {get;set;} public string? ValidationNotes {get;set;} public bool IsApprovedForUse {get;set;} public DateTime CreatedAt {get;set;} }

