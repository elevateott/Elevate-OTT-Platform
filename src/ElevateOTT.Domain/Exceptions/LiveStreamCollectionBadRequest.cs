namespace ElevateOTT.Domain.Exceptions;

public sealed class LiveStreamCollectionBadRequest : BadRequestException
{
    public LiveStreamCollectionBadRequest()
        : base("Live stream collection sent from a client is null.")
    { }
}
