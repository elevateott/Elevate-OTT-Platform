using System.ComponentModel.DataAnnotations;

namespace ElevateOTT.Domain.Entities;

public abstract class EntityBase
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
    public string ModifiedBy { get; set; } = string.Empty;
    public bool IsDeleted { get; set; } = false;

    public EntityBase()
    {
        SetCreatedAtDateTime();
    }

    protected void SetCreatedAtDateTime()
    {
        if (CreatedAt.Equals(DateTime.MinValue))
        {
            CreatedAt = DateTime.Now;
        }
    }

    protected void SetUpdatedAtDateTime()
    {
        UpdatedAt = DateTime.Now;
    }
}
