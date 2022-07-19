namespace ElevateOTT.Domain.Exceptions;

public sealed class TagNotFoundException : NotFoundException
{
    public TagNotFoundException(Guid tagId) :
        base($"The tag with id: {tagId} doesn't exist in the database.")
    { }
}
