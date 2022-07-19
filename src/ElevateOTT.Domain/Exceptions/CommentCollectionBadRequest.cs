namespace ElevateOTT.Domain.Exceptions;

public class CommentCollectionBadRequest : BadRequestException
{
    public CommentCollectionBadRequest()
        : base("Comment collection sent from a client is null.")
    { }
}
