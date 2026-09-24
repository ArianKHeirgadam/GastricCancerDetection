using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GastricCancerDetection.Domain.Entities;

[Table("AuditLog", Schema = "sec")]
public class AuditLog
{
    [Key]
    public long AuditId { get; set; }

    public int? UserId { get; set; }

    public string ActionType { get; set; } = null!;

    public string? EntityType { get; set; }

    public long? EntityId { get; set; }

    public string? OldValue { get; set; }

    public string? NewValue { get; set; }

    public string? IPAddress { get; set; }

    public DateTime CreatedAt { get; set; }
}