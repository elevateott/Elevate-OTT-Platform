namespace ElevateOTT.Application.Features.Content.Authors.Queries.GetAuthorForEdit;

public class AuthorForEdit : AuditableDto
{
    #region Public Properties
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Bio { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public string SeoTitle { get; set; } = string.Empty;
    public string SeoDescription { get; set; } = string.Empty;
    public string Permalink { get; set; } = string.Empty;

    #endregion Public Properties
}
