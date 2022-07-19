namespace ElevateOTT.Domain.Entities.Content;

[Table("CollectionVideoPositions")]
public class CollectionVideoPositionModel : EntityBase
{
    public Guid? CollectionId { get; set; }
    public Guid? VideoId { get; set; }
    public int Position { get; set; }

    #region navigational properties
    public CollectionModel? Collection { get; set; }
    public VideoModel? Video { get; set; }
    #endregion
}
