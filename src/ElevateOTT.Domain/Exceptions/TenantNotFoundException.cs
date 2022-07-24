using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevateOTT.Domain.Exceptions;
public sealed class TenantNotFoundException : NotFoundException
{
    public TenantNotFoundException() :
        base($"The tenant was not found.")
    { }
}
