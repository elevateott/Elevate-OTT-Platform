namespace ElevateOTT.Domain.Entities.Content;

[Table("Categories")]
public class CategoryModel : BaseEntity
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
    public ICollection<VideoCategoryModel>? VideosCategories { get; set; }
    public ICollection<CategoryCollectionModel>? CategoriesCollections { get; set; }
    public SeoMetaDataModel? SeoMetaData { get; set; }
    #endregion
}
