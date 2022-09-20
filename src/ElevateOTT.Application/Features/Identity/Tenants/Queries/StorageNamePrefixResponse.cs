using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevateOTT.Application.Features.Identity.Tenants.Queries;

public class StorageNamePrefixResponse
{
    public Guid? TenantId { get; set; }

    public string? StorageFileNamePrefix { get; set; } 

}
