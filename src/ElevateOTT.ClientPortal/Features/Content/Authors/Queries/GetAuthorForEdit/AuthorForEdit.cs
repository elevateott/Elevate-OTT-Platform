namespace ElevateOTT.ClientPortal.Features.Content.Authors.Queries.GetAuthorForEdit;

public class AuthorForEdit
{
    #region Public Properties
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Bio { get; set; } = string.Empty;
    public string? ImageUrl { get; set; }
    public string SeoTitle { get; set; } = string.Empty;
    public string SeoDescription { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public bool IsImageAdded { get; set; }
    #endregion Public Properties
}
