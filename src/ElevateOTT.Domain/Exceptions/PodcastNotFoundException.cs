namespace ElevateOTT.Domain.Exceptions;

public sealed class PodcastNotFoundException : NotFoundException
{
    public PodcastNotFoundException(Guid podcastId) :
        base($"The podcast with id: {podcastId} doesn't exist in the database.")
    { }
}
