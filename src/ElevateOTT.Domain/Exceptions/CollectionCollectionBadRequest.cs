namespace ElevateOTT.Domain.Exceptions;

public class CollectionCollectionBadRequest : BadRequestException
{
    public CollectionCollectionBadRequest()
        : base("Collection collection sent from a client is null.")
    { }
}
