using System.ComponentModel.DataAnnotations.Schema;
namespace GastricCancerDetection.Domain.Entities;
[Table("RepeatElements", Schema="ref")]
public class RepeatElement { public long RepeatElementId {get;set;} public int BuildId {get;set;} public string Chromosome {get;set;}=null!; public long Start {get;set;} public long End {get;set;} public string RepeatType {get;set;}=null!; public string? Family {get;set;} public string? ElementId {get;set;} }
