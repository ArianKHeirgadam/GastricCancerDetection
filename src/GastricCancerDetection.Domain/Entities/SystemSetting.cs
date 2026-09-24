using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace GastricCancerDetection.Domain.Entities;

[Table("SystemSettings", Schema="cfg")]
public class SystemSetting { public string SettingKey {get;set;}=null!; public string SettingValue {get;set;}=null!; public string? Description {get;set;} public int? UpdatedByUserId {get;set;} public DateTime UpdatedAt {get;set;} }

