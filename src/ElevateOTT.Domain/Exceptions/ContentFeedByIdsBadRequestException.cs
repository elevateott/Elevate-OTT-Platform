namespace ElevateOTT.Domain.Exceptions;

public class ContentFeedByIdsBadRequestException : BadRequestException
{
    public ContentFeedByIdsBadRequestException() :
        base("Collection count mismatch comparing to ids.")
    { }
}
