namespace ElevateOTT.Domain.Entities;

public class Report : IAuditable, IMustHaveTenant
{
    #region Public Properties

    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string QueryString { get; set; } = string.Empty;
    public string FileName { get; set; } = string.Empty;
    public string FileUri { get; set; } = string.Empty;
    public string ContentType { get; set; } = string.Empty;
    public int Status { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
    public DateTime CreatedOn { get; set; }
    public string ModifiedBy { get; set; } = string.Empty;
    public DateTime? ModifiedOn { get; set; }
    public string DeletedBy { get; set; } = string.Empty;
    public DateTime? DeletedOn { get; set; }
    public Guid TenantId { get; set; }

    #endregion Public Properties
}
