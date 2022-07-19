namespace ElevateOTT.Domain.Exceptions;

public class ItemByIdsBadRequestException : BadRequestException
{
    public ItemByIdsBadRequestException() :
        base("Collection count mismatch comparing to ids.")
    { }
}
