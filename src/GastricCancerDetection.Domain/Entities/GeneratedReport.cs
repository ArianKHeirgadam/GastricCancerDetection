using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace GastricCancerDetection.Domain.Entities;

[Table("GeneratedReports", Schema="analysis")]
public class GeneratedReport { public int ReportId {get;set;} public int RunId {get;set;} public string ReportFormat {get;set;}=null!; public string? FilePath {get;set;} public int? GeneratedByUserId {get;set;} public DateTime GeneratedAt {get;set;} }

