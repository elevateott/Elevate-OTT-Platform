namespace ElevateOTT.Domain.Exceptions;

public sealed class ContentFeedNotFoundException : NotFoundException
{
    public ContentFeedNotFoundException(Guid contentFeedId) :
        base($"The content feed with id: {contentFeedId} doesn't exist in the database.")
    { }
}
