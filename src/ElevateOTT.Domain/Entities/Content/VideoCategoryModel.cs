namespace ElevateOTT.Domain.Entities.Content;

[Table("VideosCategories")]
public class VideoCategoryModel : BaseEntity, IMustHaveTenant
{
    public Guid TenantId { get; set; }

    public Guid VideoId { get; set; }

    public Guid CategoryId { get; set; }

    public VideoModel? Video { get; set; }

    public CategoryModel? Category { get; set; }
}
