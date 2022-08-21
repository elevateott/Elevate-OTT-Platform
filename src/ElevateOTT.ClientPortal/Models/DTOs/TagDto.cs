namespace ElevateOTT.ClientPortal.Models.DTOs;

public class TagDto : AuditableDto
{
    public Guid TenantId { get; set; }

    public string? Name { get; set; }
}
