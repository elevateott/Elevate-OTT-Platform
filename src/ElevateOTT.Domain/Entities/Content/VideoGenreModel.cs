namespace ElevateOTT.Domain.Entities.Content;

[Table("VideosGenres")]
public class VideoGenreModel : EntityBase
{
    public Guid VideoId { get; set; }

    public Guid GenreId { get; set; }

    public VideoModel? Video { get; set; }

    public GenreModel? Genre { get; set; }
}
