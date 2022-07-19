namespace ElevateOTT.Domain.Exceptions;

public class PodcastByIdsBadRequestException : BadRequestException
{
    public PodcastByIdsBadRequestException() :
        base("Collection count mismatch comparing to ids.")
    { }
}
