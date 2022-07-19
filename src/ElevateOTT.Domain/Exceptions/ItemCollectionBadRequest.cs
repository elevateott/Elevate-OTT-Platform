namespace ElevateOTT.Domain.Exceptions;

public class ItemCollectionBadRequest : BadRequestException
{
    public ItemCollectionBadRequest()
        : base("Item collection sent from a client is null.")
    { }
}
