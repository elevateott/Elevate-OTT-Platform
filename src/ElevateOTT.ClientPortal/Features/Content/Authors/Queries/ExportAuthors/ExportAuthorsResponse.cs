namespace ElevateOTT.ClientPortal.Features.Content.Authors.Queries.ExportAuthors;

public class ExportAuthorsResponse
{
    #region Public Properties
    public string Id { get; set; } = string.Empty;
    public string SuccessMessage { get; set; } = string.Empty;
    public string FileName { get; set; } = string.Empty;
    public string ContentType { get; set; } = string.Empty;
    public string FileUrl { get; set; } = string.Empty;
    #endregion Public Properties
}
