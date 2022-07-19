namespace ElevateOTT.Domain.Exceptions;

public class LicenseCollectionBadRequest : BadRequestException
{
    public LicenseCollectionBadRequest()
        : base("License collection sent from a client is null.")
    { }
}
