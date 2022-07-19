namespace ElevateOTT.Domain.Exceptions;

public sealed class SubscriptionNotFoundException : NotFoundException
{
    public SubscriptionNotFoundException(Guid subscriptionId) :
        base($"The subscription with id: {subscriptionId} doesn't exist in the database.")
    { }
}
