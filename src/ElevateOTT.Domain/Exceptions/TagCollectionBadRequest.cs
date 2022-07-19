namespace ElevateOTT.Domain.Exceptions;

public class TagCollectionBadRequest : BadRequestException
{
    public TagCollectionBadRequest()
        : base("Tag collection sent from a client is null.")
    { }
}
