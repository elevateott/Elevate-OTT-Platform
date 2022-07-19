namespace ElevateOTT.Domain.Exceptions;

public class ItemPriceCollectionBadRequest : BadRequestException
{
    public ItemPriceCollectionBadRequest()
        : base("Item Price collection sent from a client is null.")
    { }
}
