namespace ElevateOTT.Domain.Exceptions;

public sealed class ItemNotFoundException : NotFoundException
{
    public ItemNotFoundException(Guid itemId) :
        base($"The item with id: {itemId} doesn't exist in the database.")
    { }
}
