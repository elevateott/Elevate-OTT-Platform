namespace BinaryPlate.BlazorPlate.Interfaces.Consumers;

public interface IReportsClient
{
    #region Public Methods

    Task<HttpResponseWrapper<object>> GetReport(GetReportForEditQuery request);

    Task<HttpResponseWrapper<object>> GetReports(GetReportsQuery request);

    #endregion Public Methods
}