namespace ElevateOTT.Domain.Exceptions;

public class SubscriptionByIdsBadRequestException : BadRequestException
{
    public SubscriptionByIdsBadRequestException() :
        base("Collection count mismatch comparing to ids.")
    { }
}
