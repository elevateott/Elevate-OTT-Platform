namespace ElevateOTT.Domain.Exceptions;

public class ItemPriceByIdsBadRequestException : BadRequestException
{
    public ItemPriceByIdsBadRequestException() :
        base("Collection count mismatch comparing to ids.")
    { }
}
