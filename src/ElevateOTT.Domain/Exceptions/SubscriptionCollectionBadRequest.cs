namespace ElevateOTT.Domain.Exceptions;

public class SubscriptionCollectionBadRequest : BadRequestException
{
    public SubscriptionCollectionBadRequest()
        : base("Subscription collection sent from a client is null.")
    { }
}
