namespace ElevateOTT.StreamingWebApp.Interfaces.Consumers;

public interface IApplicantsClient
{
    #region Public Methods

    Task<HttpResponseWrapper<object>> GetApplicant(GetApplicantForEditQuery request);

    Task<HttpResponseWrapper<object>> GetApplicantReferences(GetApplicantReferencesQuery request);

    Task<HttpResponseWrapper<object>> GetApplicants(GetApplicantsQuery request);

    Task<HttpResponseWrapper<object>> CreateApplicant(CreateApplicantCommand request);

    Task<HttpResponseWrapper<object>> UpdateApplicant(UpdateApplicantCommand request);

    Task<HttpResponseWrapper<object>> DeleteApplicant(string id);

    Task<HttpResponseWrapper<object>> FluentValidation(CreateApplicantCommand request);

    Task<HttpResponseWrapper<object>> GetApplicantNoAuth(GetApplicantForEditQuery request);

    Task<HttpResponseWrapper<object>> GetApplicantsNoAuth(GetApplicantsQuery request);

    Task<HttpResponseWrapper<object>> CreateApplicantNoAuth(CreateApplicantCommand request);

    Task<HttpResponseWrapper<object>> UpdateApplicantNoAuth(UpdateApplicantCommand request);

    Task<HttpResponseWrapper<object>> DeleteApplicantNoAuth(string id);

    Task<HttpResponseWrapper<object>> ExportAsPdf(ExportApplicantsQuery request);

    #endregion Public Methods
}