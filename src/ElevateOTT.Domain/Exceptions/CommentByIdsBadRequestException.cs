namespace ElevateOTT.Domain.Exceptions;

public class CommentByIdsBadRequestException : BadRequestException
{
    public CommentByIdsBadRequestException() :
        base("Collection count mismatch comparing to ids.")
    { }
}
