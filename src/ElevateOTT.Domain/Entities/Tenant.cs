namespace ElevateOTT.Domain.Entities;

public class Tenant : IAuditable
{
    #region Public Properties

<<<<<<< HEAD
    public Guid Id { get; set; }
    public string? LicenseKey { get; set; }
    public string? ChannelName { get; set; }
    public string? FullName { get; set; }
    public string? SubDomain { get; set; }
    public string? CustomDomain { get; set; }
    public string? HeardAboutUsFrom { get; set; }
=======
    public Guid Id { get; set; } 
    public string? OttChannelName { get; set; }
    public string? Name { get; set; }
    public string? CustomDomain { get; set; } 
>>>>>>> parent of 536757c (started on free trial sign up flow)
    public string? StorageFileNamePrefix { get; set; } 
    public string? CreatedBy { get; set; } 
    public DateTime CreatedOn { get; set; }
    public string? ModifiedBy { get; set; }
    public DateTime? ModifiedOn { get; set; }
    public string? DeletedBy { get; set; } 
    public DateTime? DeletedOn { get; set; }

    #endregion Public Properties
}
