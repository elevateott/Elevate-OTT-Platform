namespace ElevateOTT.Domain.Exceptions;

public class AuthorCollectionBadRequest : BadRequestException
{
    public AuthorCollectionBadRequest()
        : base("Author collection sent from a client is null.")
    {}
}
