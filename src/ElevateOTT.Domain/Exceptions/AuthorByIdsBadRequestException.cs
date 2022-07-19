namespace ElevateOTT.Domain.Exceptions;

public class AuthorByIdsBadRequestException : BadRequestException
{
    public AuthorByIdsBadRequestException() : 
        base("Collection count mismatch comparing to ids.") { }
}
