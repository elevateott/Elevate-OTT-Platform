using System.ComponentModel;
using System.Runtime.CompilerServices;
using ElevateOTT.Domain.Common.DTOs;

namespace ElevateOTT.Application.Features.Content.Categories.Queries.GetCategoryForEdit;

public class CategoryForEdit : AuditableDto
{
    public Guid Id { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public int Position { get; set; }

    public string? ImageUrl { get; set; }

    public string? SeoTitle { get; set; }

    public string? SeoDescription { get; set; }

    public string? Slug { get; set; }
}
