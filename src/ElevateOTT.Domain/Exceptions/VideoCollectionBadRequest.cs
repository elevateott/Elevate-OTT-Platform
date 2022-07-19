namespace ElevateOTT.Domain.Exceptions;

public class VideoCollectionBadRequest : BadRequestException
{
    public VideoCollectionBadRequest()
        : base("Video collection sent from a client is null.")
    { }
}
