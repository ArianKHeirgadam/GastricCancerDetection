using System.ComponentModel.DataAnnotations.Schema;
namespace GastricCancerDetection.Domain.Entities;
[Table("PMDRegions", Schema="ref")]
public class PMDRegion { public long PMDRegionId {get;set;} public int BuildId {get;set;} public string Chromosome {get;set;}=null!; public long Start {get;set;} public long End {get;set;} public long LengthBp {get;set;} public string? Source {get;set;} }
