using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevateOTT.Application.Features.Content.Videos.Queries.GetNewStorageName;

public record NewStorageNameResponse
{
    public string Name { get; set; } = string.Empty;
}
