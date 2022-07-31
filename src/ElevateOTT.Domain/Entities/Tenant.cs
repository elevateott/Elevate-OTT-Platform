namespace ElevateOTT.Domain.Entities;

public class Tenant : IAuditable
{
    #region Public Properties
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string CustomDomain { get; set; } = string.Empty;
    public string StorageFileNamePrefix { get; set; } = string.Empty;
    public string CreatedBy { get; set; } = string.Empty;
    public DateTime CreatedOn { get; set; }
    public string ModifiedBy { get; set; } = string.Empty;
    public DateTime? ModifiedOn { get; set; }
    public string DeletedBy { get; set; } = string.Empty;
    public DateTime? DeletedOn { get; set; }
    #endregion Public Properties
}
