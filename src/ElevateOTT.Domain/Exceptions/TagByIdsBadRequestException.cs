namespace ElevateOTT.Domain.Exceptions;

public class TagByIdsBadRequestException : BadRequestException
{
    public TagByIdsBadRequestException() :
        base("Collection count mismatch comparing to ids.")
    { }
}
