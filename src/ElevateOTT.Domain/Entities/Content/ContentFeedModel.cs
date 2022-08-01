using System.ComponentModel.DataAnnotations;

namespace ElevateOTT.Domain.Entities.Content;

[Table("ContentFeeds")]
public class ContentFeedModel : BaseEntity
{
    [Url]
    public string? Url { get; set; } 

    public DistributionType DistributionType { get; set; }

    public string? ProviderName { get; set; } 

    public string? Language { get; set; } 

    public string? LastUpdated { get; set; } 

    #region foreign keys
    //[ForeignKey(nameof(TenantModel))]
    //public Guid? TenantId { get; set; }
    #endregion

    #region navigational properties
    //public TenantModel? Tenant { get; set; }
    #endregion
}
