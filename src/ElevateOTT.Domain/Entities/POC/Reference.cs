namespace ElevateOTT.Domain.Entities.POC;

public class Reference : IAuditable
{
    #region Public Properties

    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string JobTitle { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public Guid ApplicantId { get; set; }
    public Applicant? Applicant { get; set; }

    public string CreatedBy { get; set; } = string.Empty;
    public DateTime CreatedOn { get; set; }
    public string ModifiedBy { get; set; } = string.Empty;
    public DateTime? ModifiedOn { get; set; }
    public string DeletedBy { get; set; } = string.Empty;
    public DateTime? DeletedOn { get; set; }

    #endregion Public Properties
}
