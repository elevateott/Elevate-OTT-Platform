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
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime? DateOfBirth { get; set; }

    
    public double Height { get; set; }

    
    public double Weight { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public string? ModifiedBy { get; set; }
    public DateTime? ModifiedOn { get; set; }
    public string? DeletedBy { get; set; }
    public DateTime? DeletedOn { get; set; }
    public Guid TenantId { get; set; }

    #endregion Public Properties


    #region Navigational Properties

    public ICollection<Reference> References { get; set; }

    #endregion Navigational Properties
}
