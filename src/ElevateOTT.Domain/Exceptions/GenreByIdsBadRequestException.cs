namespace ElevateOTT.Domain.Exceptions;

public class GenreByIdsBadRequestException : BadRequestException
{
    public GenreByIdsBadRequestException() :
        base("Collection count mismatch comparing to ids.")
    { }
}
