using System.ComponentModel.DataAnnotations.Schema;
namespace GastricCancerDetection.Domain.Entities;
[Table("MethylationRegion", Schema="geno")]
public class MethylationRegion { public long MethylationRegionId {get;set;} public int AnalysisRunId {get;set;} public int BuildId {get;set;} public string Chromosome {get;set;}=null!; public long Start {get;set;} public long End {get;set;} public double? MeanMethylation {get;set;} public double? TumorNormalDelta {get;set;} public string? RegionType {get;set;} public int? CohortCount {get;set;} public int? ValidationCount {get;set;} public string? LiteratureStatus {get;set;} }
