namespace ElevateOTT.Domain.Exceptions;

public sealed class CategoryNotFoundException : NotFoundException
{
    public CategoryNotFoundException(Guid categoryId) :
        base($"The category with id: {categoryId} doesn't exist in the database.")
    { }
}
