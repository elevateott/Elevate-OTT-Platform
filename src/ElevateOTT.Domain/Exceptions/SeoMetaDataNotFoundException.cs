namespace ElevateOTT.Domain.Exceptions;

public sealed class SeoMetaDataNotFoundException : NotFoundException
{
    public SeoMetaDataNotFoundException(Guid seoMetaDataId) :
        base($"The seo meta data with id: {seoMetaDataId} doesn't exist in the database.")
    { }
}
