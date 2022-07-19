namespace ElevateOTT.Domain.Exceptions;

public class CategoryByIdsBadRequestException : BadRequestException
{
    public CategoryByIdsBadRequestException() :
        base("Collection count mismatch comparing to ids.")
    { }
}
