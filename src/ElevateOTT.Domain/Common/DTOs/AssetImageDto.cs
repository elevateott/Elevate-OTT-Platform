using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevateOTT.Domain.Common.DTOs;

public class AssetImageDto : AuditableDto
{
    public Guid TenantId { get; set; }

    public string? Name { get; set; }

    public AssetImageType AssetImageType { get; set; }

    public string? Url { get; set; }

    public int Width { get; set; }

    public int Height { get; set; }
}
