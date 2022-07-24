namespace ElevateOTT.Domain.Entities.Content;

[Table("SeoMetaData")]
public class SeoMetaDataModel : BaseEntity
{
    public string SeoTitle { get; set; } = string.Empty;

    public string SeoDescription { get; set; } = string.Empty;

    public string Permalink { get; set; } = string.Empty;

    #region foreign keys
    [ForeignKey(nameof(VideoModel))]
    public Guid? VideoId { get; set; }

    [ForeignKey(nameof(CategoryModel))]
    public Guid? CategoryId { get; set; }

    [ForeignKey(nameof(CollectionModel))]
    public Guid? CollectionId { get; set; }
    #endregion

    #region navigational properties
    public VideoModel? Video { get; set; }
    public CategoryModel? Category { get; set; }
    public CollectionModel? Collection { get; set; }
    #endregion
}
