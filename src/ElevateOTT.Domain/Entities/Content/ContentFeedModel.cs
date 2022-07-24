using System.ComponentModel.DataAnnotations;

namespace ElevateOTT.Domain.Entities.Content;

[Table("ContentFeeds")]
public class ContentFeedModel : BaseEntity
{
    [Url]
    public string Url { get; set; } = string.Empty;

    public DistributionType DistributionType { get; set; }

    public string ProviderName { get; set; } = string.Empty;

    public string Language { get; set; } = string.Empty;

    public string LastUpdated { get; set; } = string.Empty;

    #region foreign keys
    //[ForeignKey(nameof(TenantModel))]
    //public Guid? TenantId { get; set; }
    #endregion

    #region navigational properties
    //public TenantModel? Tenant { get; set; }
    #endregion
}
