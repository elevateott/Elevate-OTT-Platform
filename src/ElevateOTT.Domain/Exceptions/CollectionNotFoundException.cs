namespace ElevateOTT.Domain.Exceptions;

public sealed class CollectionNotFoundException : NotFoundException
{
    public CollectionNotFoundException(Guid collectionId) :
        base($"The collection with id: {collectionId} doesn't exist in the database.")
    { }
}
