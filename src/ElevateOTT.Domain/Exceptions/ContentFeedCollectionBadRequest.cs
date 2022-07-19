namespace ElevateOTT.Domain.Exceptions;

public class ContentFeedCollectionBadRequest : BadRequestException
{
    public ContentFeedCollectionBadRequest()
        : base("Content feed collection sent from a client is null.")
    { }
}
