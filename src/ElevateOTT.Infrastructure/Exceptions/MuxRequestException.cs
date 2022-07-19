using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevateOTT.Infrastructure.Exceptions;

public class MuxRequestException : ArgumentNullException
{
    public MuxRequestException(string parameterName) :
        base($"The author with id: {parameterName} doesn't exist in the database.")
    { }
}
