using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevateOTT.ClientPortal.Models.DTOs;

public class CategoryDto : AuditableDto
{
    public Guid TenantId { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public int Position { get; set; }

    public string? ImageUrl { get; set; }

    public string? SeoTitle { get; set; }

    public string? SeoDescription { get; set; }

    public string? Slug { get; set; }
}
