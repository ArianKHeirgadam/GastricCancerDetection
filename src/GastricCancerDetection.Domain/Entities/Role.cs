using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace GastricCancerDetection.Domain.Entities;

[Table("Roles", Schema="sec")]
public class Role { public int RoleId {get;set;} public string RoleName {get;set;} = null!; }

