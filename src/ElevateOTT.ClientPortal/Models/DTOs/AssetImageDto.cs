﻿namespace ElevateOTT.ClientPortal.Models.DTOs;

public class AssetImageDto : AuditableDto
{
    public Guid TenantId { get; set; }

    public string? Name { get; set; }

    public AssetImageType AssetImageType { get; set; }

    public string? Url { get; set; }

    public int Width { get; set; }

    public int Height { get; set; }
}
