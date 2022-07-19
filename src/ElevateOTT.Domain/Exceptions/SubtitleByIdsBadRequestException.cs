namespace ElevateOTT.Domain.Exceptions;

public class SubtitleByIdsBadRequestException : BadRequestException
{
    public SubtitleByIdsBadRequestException() :
        base("Collection count mismatch comparing to ids.")
    { }
}
