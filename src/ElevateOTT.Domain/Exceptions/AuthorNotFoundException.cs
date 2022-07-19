namespace ElevateOTT.Domain.Exceptions;

public sealed class AuthorNotFoundException : NotFoundException
{
    public AuthorNotFoundException(Guid authorId) : 
        base($"The author with id: {authorId} doesn't exist in the database.") 
    { }
}
