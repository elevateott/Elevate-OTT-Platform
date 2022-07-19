namespace ElevateOTT.Domain.Exceptions;

public class CategoryCollectionBadRequest : BadRequestException
{
    public CategoryCollectionBadRequest()
        : base("Category collection sent from a client is null.")
    { }
}
