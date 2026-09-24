using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace GastricCancerDetection.Domain.Entities;

[Table("Users", Schema="sec")]
public class User { public int UserId {get;set;} public string FullName {get;set;}=null!; public string Email {get;set;}=null!; public string PasswordHash {get;set;}=null!; public string? PasswordSalt {get;set;} public int RoleId {get;set;} public bool IsActive {get;set;} public DateTime CreatedAt {get;set;} public DateTime? LastLoginAt {get;set;} public Role? Role {get;set;} }

