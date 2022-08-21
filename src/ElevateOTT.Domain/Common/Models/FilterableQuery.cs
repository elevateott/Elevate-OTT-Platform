namespace ElevateOTT.Domain.Common.Models;

public abstract class FilterableQuery
{
    #region Public Properties

    public string SearchText { get; set; } = string.Empty;

    public string SortBy { get; set; } = string.Empty;

    public int PageNumber { get; set; }

    /// <summary>
    /// Maximum total number of records for each page.
    /// </summary>
    public int PageSize { get; set; }

    /// <summary>
    /// Actual total number of records of the selected page.
    /// </summary>
    public int SelectedPageSize { get; set; }

    #endregion Public Properties
}
