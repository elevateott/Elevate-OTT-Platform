namespace ElevateOTT.Domain.Exceptions;

public sealed class SubtitleNotFoundException : NotFoundException
{
    public SubtitleNotFoundException(Guid subtitleId) :
        base($"The subtitle with id: {subtitleId} doesn't exist in the database.")
    { }
}
