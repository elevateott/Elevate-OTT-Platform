namespace ElevateOTT.Domain.Exceptions;

public class SeoMetaDataCollectionBadRequest : BadRequestException
{
    public SeoMetaDataCollectionBadRequest()
        : base("SEO meta data collection sent from a client is null.")
    { }
}
