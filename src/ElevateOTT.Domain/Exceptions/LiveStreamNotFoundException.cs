namespace ElevateOTT.Domain.Exceptions;

public sealed class LiveStreamNotFoundException : NotFoundException
{
    public LiveStreamNotFoundException(Guid videoId) :
        base($"The live stream with id: {videoId} doesn't exist in the database.")
    { }

    public LiveStreamNotFoundException() :
        base($"The live stream requested doesn't exist in the database.")
    { }
}
