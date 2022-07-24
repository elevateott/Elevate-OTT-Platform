namespace ElevateOTT.ClientPortal.Features.Content.Authors.Queries.ExportAuthors
{
    public class ExportAuthorsQuery
    {
        #region Public Properties
        public string SearchText { get; set; } = string.Empty;
        public string SortBy { get; set; } = string.Empty;
        public bool IsImmediate { get; set; } = true;
        #endregion Public Properties
    }
}
