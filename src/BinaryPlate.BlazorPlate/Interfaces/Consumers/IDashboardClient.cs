namespace BinaryPlate.BlazorPlate.Interfaces.Consumers;

public interface IDashboardClient
{
    #region Public Methods

    Task<HttpResponseWrapper<object>> GetHeadlinesData();

    #endregion Public Methods
}