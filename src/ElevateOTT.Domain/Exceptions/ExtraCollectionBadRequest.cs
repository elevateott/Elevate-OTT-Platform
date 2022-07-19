namespace ElevateOTT.Domain.Exceptions;

public class ExtraCollectionBadRequest : BadRequestException
{
    public ExtraCollectionBadRequest()
        : base("Extra collection sent from a client is null.")
    { }
}
