namespace ElevateOTT.Domain.Exceptions;

public class SubtitleCollectionBadRequest : BadRequestException
{
    public SubtitleCollectionBadRequest()
        : base("Subtitle collection sent from a client is null.")
    { }
}
