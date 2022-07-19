namespace ElevateOTT.Domain.Exceptions;

public class PodcastCollectionBadRequest : BadRequestException
{
    public PodcastCollectionBadRequest()
        : base("Podcast collection sent from a client is null.")
    { }
}
