namespace BinaryPlate.Domain.Common.Interfaces;

/// <summary>
/// Indicates that any class inherits this interface has audit properties as shadow properties.
/// </summary>
/// <remarks>
/// This interface is only allowed to be used with entity classes. This interface is not allowed to
/// be used with DTO classes.
/// </remarks>
public interface IAuditable
{
    #region Public Properties

    public string CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public string ModifiedBy { get; set; }
    public DateTime? ModifiedOn { get; set; }
    public string DeletedBy { get; set; }
    public DateTime? DeletedOn { get; set; }

    #endregion Public Properties
}