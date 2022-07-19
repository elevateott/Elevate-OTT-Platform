namespace ElevateOTT.Domain.Entities.Content;

[Table("Tags")]
public class TagModel : EntityBase
{
    public string Name { get; set; } = string.Empty;

    #region foreign keys
    //[ForeignKey(nameof(TenantModel))]
    //public Guid? TenantId { get; set; }
    #endregion

    #region navigational properties
    //public TenantModel? Tenant { get; set; }

    public ICollection<VideoTagModel>? VideosTags { get; set; }
    #endregion
}
