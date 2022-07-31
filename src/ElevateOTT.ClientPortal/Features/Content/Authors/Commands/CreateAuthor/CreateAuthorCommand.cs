namespace ElevateOTT.ClientPortal.Features.Content.Authors.Commands.CreateAuthor;

public class CreateAuthorCommand
{
    #region Public Properties
    public string Name { get; set; } = string.Empty;
    public string Bio { get; set; } = string.Empty;
    public string? ImageUrl { get; set; }
    public string SeoTitle { get; set; } = string.Empty;
    public string SeoDescription { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    #endregion Public Properties
}
