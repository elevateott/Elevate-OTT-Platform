namespace BinaryPlate.Application.Common.Interfaces.Services;

public interface ITenantResolver
{
    #region Public Properties

    TenantMode TenantMode { get; set; }

    #endregion Public Properties

    #region Public Methods

    Guid? GetTenantId();

    void SetTenantId(Guid? tenantId);

    string GetTenantName();

    void SetTenantName(string tenantName);

    #endregion Public Methods
}