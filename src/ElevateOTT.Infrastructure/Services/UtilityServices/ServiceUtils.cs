using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElevateOTT.Application.Common.Interfaces.Services.UtilityServices;

namespace ElevateOTT.Infrastructure.Services.UtilityServices;
public class ServiceUtils : IServiceUtils
{
    private readonly Random _random;

    public ServiceUtils()
    {
        _random = new Random();
    }

    public bool VerifyIsHls(string url)
    {
        throw new NotImplementedException();
    }

    public DateTime UnixTimeToDateTime(long unixtime)
    {
        throw new NotImplementedException();
    }

    public long DateTimeToUnix(DateTime MyDateTime)
    {
        throw new NotImplementedException();
    }

    public string EncodeTo64(string toEncode)
    {
        throw new NotImplementedException();
    }

    public string DecodeBase64(string toDecode)
    {
        throw new NotImplementedException();
    }

    // Generates a random string with a given size.    
    public string RandomString(int size, bool lowerCase = false)
    {
        var builder = new StringBuilder(size);

        // Unicode/ASCII Letters are divided into two blocks
        // (Letters 65–90 / 97–122):   
        // The first group containing the uppercase letters and
        // the second group containing the lowercase.  

        // char is a single Unicode character  
        char offset = lowerCase ? 'a' : 'A';
        const int lettersOffset = 26; // A...Z or a..z: length = 26  

        for (var i = 0; i < size; i++)
        {
            var @char = (char)_random.Next(offset, offset + lettersOffset);
            builder.Append(@char);
        }

        return lowerCase ? builder.ToString().ToLower() : builder.ToString();
    }
}
