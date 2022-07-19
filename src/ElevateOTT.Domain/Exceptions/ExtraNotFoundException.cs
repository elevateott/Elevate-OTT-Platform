namespace ElevateOTT.Domain.Exceptions;

public sealed class ExtraNotFoundException : NotFoundException
{
    public ExtraNotFoundException(Guid extraId) :
        base($"The extra with id: {extraId} doesn't exist in the database.")
    { }
}
