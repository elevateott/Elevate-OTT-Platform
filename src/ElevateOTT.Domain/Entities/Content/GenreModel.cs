namespace ElevateOTT.Domain.Entities.Content;

[Table("Genres")]
public class GenreModel : BaseEntity
{
    public string Name { get; set; } = string.Empty;

    #region foreign keys
    //[ForeignKey(nameof(TenantModel))]
    //public Guid? TenantId { get; set; }
    #endregion

    #region navigational properties
    //public TenantModel? Tenant { get; set; }
    public ICollection<VideoGenreModel>? VideosGenres { get; set; }
    #endregion
}
