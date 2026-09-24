using System.ComponentModel.DataAnnotations.Schema;
namespace GastricCancerDetection.Domain.Entities;
[Table("MitoRegions", Schema="ref")]
public class MitoRegion { public long MitoRegionId {get;set;} public string RegionName {get;set;}=null!; public long Start {get;set;} public long End {get;set;} public string? FeatureType {get;set;} }
