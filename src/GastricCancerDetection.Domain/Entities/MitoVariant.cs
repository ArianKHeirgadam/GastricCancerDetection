using System.ComponentModel.DataAnnotations.Schema;
namespace GastricCancerDetection.Domain.Entities;
[Table("MitoVariants", Schema="geno")]
public class MitoVariant { public long MitoVariantId {get;set;} public int AnalysisRunId {get;set;} public long Position {get;set;} public string RefAllele {get;set;}=null!; public string AltAllele {get;set;}=null!; public int? Depth {get;set;} public double? Vaf {get;set;} public double? Heteroplasmy {get;set;} public double? PopulationFrequency {get;set;} public string? Haplogroup {get;set;} public double? CopyNumber {get;set;} }
