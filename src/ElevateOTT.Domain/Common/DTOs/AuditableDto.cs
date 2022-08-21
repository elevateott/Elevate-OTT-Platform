﻿namespace ElevateOTT.Domain.Common.DTOs;

/// <summary>
/// Indicates that any class inherits this interface has audit properties as shadow properties.
/// </summary>
/// <remarks>
/// This class is only allowed to be used with DTO's classes that should return audit data. This
/// class is not allowed to be used with entity classes.
/// </remarks>
public abstract class AuditableDto
{
    #region Public Properties

    public DateTime CreatedOn { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
    public DateTime? ModifiedOn { get; set; }
    public string ModifiedBy { get; set; } = string.Empty;
    public string DeletedBy { get; set; } = string.Empty;
    public DateTime? DeletedOn { get; set; }

    #endregion Public Properties
}
