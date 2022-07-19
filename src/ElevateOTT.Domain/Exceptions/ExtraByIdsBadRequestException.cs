namespace ElevateOTT.Domain.Exceptions;

public class ExtraByIdsBadRequestException : BadRequestException
{
    public ExtraByIdsBadRequestException() :
        base("Collection count mismatch comparing to ids.")
    { }
}
