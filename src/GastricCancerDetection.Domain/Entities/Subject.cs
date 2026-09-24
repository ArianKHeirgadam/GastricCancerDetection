using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace GastricCancerDetection.Domain.Entities;

[Table("Subjects", Schema="clin")]
public class Subject { public int SubjectId {get;set;} public string SubjectCode {get;set;}=null!; public int? InstitutionId {get;set;} public string? Gender {get;set;} public int? BirthYear {get;set;} public string? Ethnicity {get;set;} public bool IsHealthy {get;set;} public string? DiagnosisICD10 {get;set;} public string? DiagnosisNotes {get;set;} public bool? FamilyHistoryCancer {get;set;} public string? SmokingStatus {get;set;} public string? HPyloriStatus {get;set;} public string? SourceDatabase {get;set;} public bool ConsentObtained {get;set;} public DateTime? ConsentDate {get;set;} public bool IsDeleted {get;set;} public int? EnrolledByUserId {get;set;} public DateTime ValidFrom {get;set;} public DateTime ValidTo {get;set;} }

