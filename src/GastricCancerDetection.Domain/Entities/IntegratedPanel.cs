using System.ComponentModel.DataAnnotations.Schema;
namespace GastricCancerDetection.Domain.Entities;
[Table("IntegratedPanels", Schema="analysis")]
public class IntegratedPanel { public long IntegratedPanelId {get;set;} public int AnalysisRunId {get;set;} public string PanelName {get;set;}=null!; public string? FeatureSetJson {get;set;} public double? Score {get;set;} public string? Status {get;set;} }
