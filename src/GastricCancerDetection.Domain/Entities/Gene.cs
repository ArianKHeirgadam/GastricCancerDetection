using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace GastricCancerDetection.Domain.Entities;

[Table("Genes", Schema="geno")]
public class Gene { public int GeneId {get;set;} public string GeneSymbol {get;set;}=null!; public int BuildId {get;set;} public string Chromosome {get;set;}=null!; public long StartPosition {get;set;} public long EndPosition {get;set;} public string? Strand {get;set;} public string? EnsemblGeneId {get;set;} public string? NCBIGeneId {get;set;} public string? Description {get;set;} }

