using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace GastricCancerDetection.Domain.Entities;

[Table("Samples", Schema="clin")]
public class Sample { public int SampleId {get;set;} public int SubjectId {get;set;} public string SampleCode {get;set;}=null!; public byte? SampleTypeId {get;set;} public DateTime? CollectionDate {get;set;} public byte? PlatformId {get;set;} public string? SequencingType {get;set;} public double? MeanCoverageDepth {get;set;} public string? LabName {get;set;} public bool? QCPassed {get;set;} public string? QCNotes {get;set;} public DateTime CreatedAt {get;set;} }

