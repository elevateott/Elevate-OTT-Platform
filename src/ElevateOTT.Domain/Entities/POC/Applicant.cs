﻿namespace ElevateOTT.Domain.Entities.POC;

public class Applicant : IAuditable, IMustHaveTenant
{
    #region Public Constructors

    public Applicant()
    {
        References = new List<Reference>();
    }

    #endregion Public Constructors

    #region Public Properties

    public Guid Id { get; set; }
    public int Ssn { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime? DateOfBirth { get; set; }
    public decimal Height { get; set; }
    public decimal Weight { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
    public DateTime CreatedOn { get; set; }
    public string ModifiedBy { get; set; } = string.Empty;
    public DateTime? ModifiedOn { get; set; }
    public string DeletedBy { get; set; } = string.Empty;
    public DateTime? DeletedOn { get; set; }
    public Guid TenantId { get; set; }

    #endregion Public Properties

    #region Private Properties

    public ICollection<Reference>? References { get; set; }

    #endregion Private Properties
}
