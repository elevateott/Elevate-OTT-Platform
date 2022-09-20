using System.ComponentModel.DataAnnotations;

namespace ElevateOTT.Domain.Entities.Content;

[Table("ContentFeeds")]
public class ContentFeedModel : BaseEntity, IMustHaveTenant 
{
    // TODO 
    // User will be able to select language code form drop down in settings

    public Guid TenantId { get; set; }

    [Url]
    public string? Url { get; set; }

    public string? ProviderName { get; set; } 

    public string? LanguageCode { get; set; }

    public int Version { get; set; }
}
