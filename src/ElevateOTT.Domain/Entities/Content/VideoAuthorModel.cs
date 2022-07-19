namespace ElevateOTT.Domain.Entities.Content;

[Table("VideosAuthors")]
public class VideoAuthorModel : EntityBase
{
    public Guid VideoId { get; set; }

    public Guid AuthorId { get; set; }

    public VideoModel? Video { get; set; }

    public AuthorModel? Author { get; set; }
}
