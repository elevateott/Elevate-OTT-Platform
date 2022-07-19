namespace ElevateOTT.Domain.Exceptions;

public sealed class LicenseNotFoundException : NotFoundException
{
    public LicenseNotFoundException(Guid licenseId) :
        base($"The license with id: {licenseId} doesn't exist in the database.")
    { }
}
