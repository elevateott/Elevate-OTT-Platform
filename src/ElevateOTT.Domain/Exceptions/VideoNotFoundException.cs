namespace ElevateOTT.Domain.Exceptions;

public sealed class VideoNotFoundException : NotFoundException
{
    public VideoNotFoundException(Guid videoId) :
        base($"The video with id: {videoId} doesn't exist in the database.")
    { }

    public VideoNotFoundException() :
        base($"The video requested doesn't exist in the database.")
    { }
}
