﻿using ElevateOTT.Domain.Common.DTOs;

namespace ElevateOTT.Application.Features.POC.Applicants.Queries.GetApplicants;

public class ApplicantItem : AuditableDto
{
    #region Public Properties

    public string Id { get; set; } = string.Empty;
    public int Ssn { get; set; } 
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string FullName => $"{FirstName} {LastName}";
    public DateTime? DateOfBirth { get; set; }
    public double Height { get; set; }
    public double Weight { get; set; }
    public List<ApplicantReferenceItem> References { get; set; }

    public double Bmi
    {
        get => Height != 0 ? Weight / (Height / 100 * 2) : 0;
        set { if (value <= 0) throw new ArgumentOutOfRangeException(nameof(value)); }
    }

    #endregion Public Properties

    #region Public Methods

    public static ApplicantItem MapFromEntity(Applicant applicant)
    {
        return new()
        {
            Id = applicant.Id.ToString(),
            Ssn = applicant.Ssn,
            FirstName = applicant.FirstName,
            LastName = applicant.LastName,
            DateOfBirth = applicant.DateOfBirth,
            Height = applicant.Height,
            Weight = applicant.Weight,
            CreatedOn = applicant.CreatedOn,
            CreatedBy = applicant.CreatedBy,
            ModifiedOn = applicant.ModifiedOn,
            ModifiedBy = applicant.ModifiedBy,
            References = applicant.References.Select(r => new ApplicantReferenceItem
            {
                Name = r.Name,
                JobTitle = r.JobTitle,
                Phone = r.Phone,
                CreatedBy = r.CreatedBy,
                CreatedOn = r.CreatedOn,
                ModifiedBy = r.ModifiedBy,
                ModifiedOn = r.ModifiedOn
            }).ToList()
        };
    }

    #endregion Public Methods
}
