using System.ComponentModel.DataAnnotations.Schema;
namespace GastricCancerDetection.Domain.Entities;
[Table("MethylationCall", Schema="geno")]
public class MethylationCall { public long MethylationCallId {get;set;} public int AnalysisRunId {get;set;} public int BuildId {get;set;} public string Chromosome {get;set;}=null!; public long Position {get;set;} public double? BetaValue {get;set;} public double? Coverage {get;set;} public string? SampleType {get;set;} }
