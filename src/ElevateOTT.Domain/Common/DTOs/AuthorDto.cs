using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevateOTT.Domain.Common.DTOs;

public class AuthorDto : AuditableDto
{
    public Guid Id { get; set; }

    public Guid TenantId { get; set; }

    public string? Name { get; set; }

    public string? Bio { get; set; }

    public string? ImageUrl { get; set; }

    public string? SeoTitle { get; set; }

    public string? SeoDescription { get; set; }

    public string? Slug { get; set; }
}
