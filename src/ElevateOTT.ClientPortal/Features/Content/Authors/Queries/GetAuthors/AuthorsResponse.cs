namespace ElevateOTT.ClientPortal.Features.Content.Authors.Queries.GetAuthors;

public class AuthorsResponse
{
    #region Public Properties
    public PagedList<AuthorDto>? Authors { get; set; }
    #endregion Public Properties
}
