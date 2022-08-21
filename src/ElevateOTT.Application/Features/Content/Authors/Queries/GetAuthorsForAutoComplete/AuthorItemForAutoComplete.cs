using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevateOTT.Application.Features.Content.Authors.Queries.GetAuthorsForAutoComplete;

public class AuthorItemForAutoComplete
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
}
