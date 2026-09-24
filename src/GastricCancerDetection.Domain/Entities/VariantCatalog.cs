using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace GastricCancerDetection.Domain.Entities;

[Table("VariantCatalog", Schema="geno")]
public class VariantCatalog { public long VariantCatalogId {get;set;} public int BuildId {get;set;} public string Chromosome {get;set;}=null!; public long Position {get;set;} public string RefAllele {get;set;}=null!; public string AltAllele {get;set;}=null!; public string? dbSNP_Id {get;set;} public int? GeneId {get;set;} public int? TranscriptId {get;set;} public string? ClinVar_Id {get;set;} public byte? ClinSigId {get;set;} public byte? ConsequenceId {get;set;} public string? ProteinChange {get;set;} public double? SIFT_Score {get;set;} public double? PolyPhen_Score {get;set;} public double? CADD_Score {get;set;} public DateTime CreatedAt {get;set;} }

