namespace ElevateOTT.Domain.Exceptions;

public class LicenseByIdsBadRequestException : BadRequestException
{
    public LicenseByIdsBadRequestException() :
        base("Collection count mismatch comparing to ids.")
    { }
}
