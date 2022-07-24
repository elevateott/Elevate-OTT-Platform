namespace ElevateOTT.Domain.Entities.Identity;

public class ApplicationUserAttachment
{
    #region Public Properties

    public Guid Id { get; set; }
    public string FileUri { get; set; } = string.Empty;
    public string FileName { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public ApplicationUser? User { get; set; }

    #endregion Public Properties
}
