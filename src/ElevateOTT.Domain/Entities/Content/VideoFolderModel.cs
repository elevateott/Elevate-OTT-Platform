using System.ComponentModel.DataAnnotations;

namespace ElevateOTT.Domain.Entities.Content;

[Table("VideoFolders")]
public class VideoFolderModel : BaseEntity
{
    [Required(ErrorMessage = "Name is a required field.")]
    [MaxLength(50, ErrorMessage = "Maximum length for the Name is 50 characters.")]
    public string? Name { get; set; }     
    public Guid? ParentId { get; set; }

    #region foreign keys
    //[ForeignKey(nameof(TenantModel))]
    //public Guid? TenantId { get; set; }
    #endregion

    #region navigational properties
    //public TenantModel? Tenant { get; set; }

    public List<VideoModel>? Videos { get; set; }
    #endregion
}
