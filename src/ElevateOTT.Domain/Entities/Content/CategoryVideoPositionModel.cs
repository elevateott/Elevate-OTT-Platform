namespace ElevateOTT.Domain.Entities.Content;

[Table("CategoryVideoPositions")]
public class CategoryVideoPositionModel : BaseEntity
{
    public Guid? CategoryId { get; set; }
    public Guid? VideoId { get; set; }
    public int Position { get; set; }

    #region navigational properties
    public CategoryModel? Category { get; set; }
    public VideoModel? Video { get; set; }
    #endregion
}
