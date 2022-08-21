using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevateOTT.Application.Features.Content.Authors.Queries.GetAuthorsForAutoComplete;

public class AuthorsForAutoCompleteResponse
{
    #region Public Properties

    public PagedList<AuthorItemForAutoComplete>? Authors { get; set; }

    #endregion Public Properties
}
