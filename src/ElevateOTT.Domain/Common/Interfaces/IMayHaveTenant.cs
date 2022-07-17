namespace ElevateOTT.Domain.Common.Interfaces;

public interface IMayHaveTenant
{
    #region Public Properties

    Guid? TenantId { get; set; }

    #endregion Public Properties
}