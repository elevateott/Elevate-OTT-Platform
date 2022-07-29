using System.ComponentModel.DataAnnotations;
using ElevateOTT.Domain.Entities.Content;

namespace ElevateOTT.Application.Features.Content.Authors.Queries.GetAuthors;

public class AuthorItem : AuditableDto
{
    //private IMapper _mapper;

    //public AuthorDto(IMapper mapper)
    //{
    //    _mapper = mapper;
    //}

    #region Public Properties

    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Bio { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public string SeoTitle { get; set; } = string.Empty;
    public string SeoDescription { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;

    #endregion Public Properties

    #region Public Methods

    //public AuthorDto MapFromEntity(AuthorModel author)
    //{
    //    return _mapper.Map<AuthorDto>(author);
    //}

    #endregion Public Methods
}
