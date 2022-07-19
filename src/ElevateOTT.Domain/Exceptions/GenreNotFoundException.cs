namespace ElevateOTT.Domain.Exceptions;

public sealed class GenreNotFoundException : NotFoundException
{
    public GenreNotFoundException(Guid genreId) :
        base($"The genre with id: {genreId} doesn't exist in the database.")
    { }
}
