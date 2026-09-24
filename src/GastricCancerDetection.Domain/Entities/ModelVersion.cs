using System.ComponentModel.DataAnnotations.Schema;
namespace GastricCancerDetection.Domain.Entities;
[Table("ModelVersions", Schema="analysis")]
public class ModelVersion { public int ModelVersionId {get;set;} public string VersionName {get;set;}=null!; public string PipelineVersion {get;set;}=null!; public string? Description {get;set;} public string ParametersJson {get;set;}="{}"; public bool IsActive {get;set;} public DateTime CreatedAt {get;set;} }
