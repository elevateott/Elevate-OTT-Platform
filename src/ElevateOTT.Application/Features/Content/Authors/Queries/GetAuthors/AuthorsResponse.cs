namespace ElevateOTT.Application.Features.Content.Authors.Queries.GetAuthors;

public class AuthorsResponse
{
    #region Public Properties
    public PagedList<AuthorItem>? Authors { get; set; }
    #endregion Public Properties
}
