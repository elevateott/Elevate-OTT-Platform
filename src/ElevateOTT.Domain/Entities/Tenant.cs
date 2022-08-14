namespace ElevateOTT.Domain.Entities;

public class Tenant : IAuditable
{
    #region Public Properties

    public Guid Id { get; set; } 
    public string? OttChannelName { get; set; }
    public string? Name { get; set; }
    public string? CustomDomain { get; set; } 
    public string? StorageFileNamePrefix { get; set; } 
    public string? CreatedBy { get; set; } 
    public DateTime CreatedOn { get; set; }
    public string? ModifiedBy { get; set; }
    public DateTime? ModifiedOn { get; set; }
    public string? DeletedBy { get; set; } 
    public DateTime? DeletedOn { get; set; }

    #endregion Public Properties
}
