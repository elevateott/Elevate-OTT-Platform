namespace ElevateOTT.Domain.Exceptions;

public class GenreCollectionBadRequest : BadRequestException
{
    public GenreCollectionBadRequest()
        : base("Genre collection sent from a client is null.")
    { }
}
