using System.ComponentModel.DataAnnotations;

namespace ElevateOTT.Domain.Entities.Content;

[Table("Extras")]
public class ExtraModel : BaseEntity 
{
    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    [Url]
    public string Url { get; set; } = string.Empty; 

    #region foreign keys
    [ForeignKey(nameof(VideoModel))]
    public Guid? VideoId { get; set; }
    #endregion

    #region navigational properties
    public VideoModel? Video { get; set; }
    #endregion
}
