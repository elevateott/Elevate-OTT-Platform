﻿namespace ElevateOTT.ClientPortal.Features.POC.Applicants.Queries.GetApplicants;

public class ApplicantItem : AuditableDto
{
    #region Public Properties

    public string Id { get; set; }
    public int Ssn { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FullName => $"{FirstName} {LastName}";
    public DateTime? DateOfBirth { get; set; }
    public double Height { get; set; }
    public double Weight { get; set; }

    public double Bmi
    {
        get => Height != 0 ? Weight / (Height / 100 * 2) : 0;
        set { if (value <= 0) throw new ArgumentOutOfRangeException(nameof(value)); }
    }

    #endregion Public Properties
}
