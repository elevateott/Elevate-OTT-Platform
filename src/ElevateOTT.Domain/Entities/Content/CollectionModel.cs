namespace ElevateOTT.Domain.Entities.Content;

[Table("Collections")]
public class CollectionModel : BaseEntity
{
    public string? Title { get; set; } 

    public string? Description { get; set; } 

    public int Position { get; set; }

    public string? ImageUrl { get; set; } 

    #region foreign keys
    //[ForeignKey(nameof(TenantModel))]
    //public Guid? TenantId { get; set; }
    #endregion

    #region navigational properties
    //public TenantModel? Tenant { get; set; }
    public List<VideoModel>? Videos { get; set; }
    public ICollection<CategoryCollectionModel>? CategoriesCollections { get; set; }
    public SeoMetaDataModel? SeoMetaData { get; set; }
    #endregion
}
