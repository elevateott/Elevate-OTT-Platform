namespace ElevateOTT.Domain.Exceptions;

public sealed class ItemPriceNotFoundException : NotFoundException
{
    public ItemPriceNotFoundException(Guid itemPriceId) :
        base($"The item price with id: {itemPriceId} doesn't exist in the database.")
    { }
}
