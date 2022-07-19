namespace ElevateOTT.Domain.Exceptions;

public class SeoMetaDataByIdsBadRequestException : BadRequestException
{
    public SeoMetaDataByIdsBadRequestException() :
        base("Collectioncount mismatch comparing to ids.")
    { }
}
