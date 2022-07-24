﻿using System.ComponentModel.DataAnnotations;

namespace ElevateOTT.Domain.Entities;

public abstract class BaseEntity : IAuditable
{
    [Key]
    public Guid Id { get; set; }

    public string CreatedBy { get; set; } = string.Empty;
    public DateTime CreatedOn { get; set; }
    public string ModifiedBy { get; set; } = string.Empty;
    public DateTime? ModifiedOn { get; set; }
    public string DeletedBy { get; set; } = string.Empty;
    public DateTime? DeletedOn { get; set; }
}
