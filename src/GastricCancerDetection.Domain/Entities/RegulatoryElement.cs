using System.ComponentModel.DataAnnotations.Schema;
namespace GastricCancerDetection.Domain.Entities;
[Table("RegulatoryElements", Schema="ref")]
public class RegulatoryElement { public long RegulatoryElementId {get;set;} public int BuildId {get;set;} public string Chromosome {get;set;}=null!; public long Start {get;set;} public long End {get;set;} public string ElementType {get;set;}=null!; public string? Source {get;set;} public string? ElementId {get;set;} public string? Gene {get;set;} public string? Annotation {get;set;} }
