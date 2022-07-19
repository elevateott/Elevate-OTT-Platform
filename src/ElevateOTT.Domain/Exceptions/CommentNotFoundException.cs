namespace ElevateOTT.Domain.Exceptions;

public sealed class CommentNotFoundException : NotFoundException
{
    public CommentNotFoundException(Guid commentId) :
        base($"The comment with id: {commentId} doesn't exist in the database.")
    { }
}
