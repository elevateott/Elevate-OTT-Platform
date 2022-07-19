namespace ElevateOTT.Domain.Entities.Content;

[Table("VideosTags")]
public class VideoTagModel : EntityBase
{
    public Guid VideoId { get; set; }

    public Guid TagId { get; set; }

    public VideoModel? Video { get; set; }

    public TagModel? Tag { get; set; }
}
