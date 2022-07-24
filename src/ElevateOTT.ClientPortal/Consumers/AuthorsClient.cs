namespace ElevateOTT.ClientPortal.Consumers;

public class AuthorsClient : IAuthorsClient
{
    public Task<HttpResponseWrapper<object>> GetApplicant(GetApplicantForEditQuery request)
    {
        throw new NotImplementedException();
    }

    public Task<HttpResponseWrapper<object>> GetApplicantReferences(GetApplicantReferencesQuery request)
    {
        throw new NotImplementedException();
    }

    public Task<HttpResponseWrapper<object>> GetApplicants(GetApplicantsQuery request)
    {
        throw new NotImplementedException();
    }

    public Task<HttpResponseWrapper<object>> CreateApplicant(CreateApplicantCommand request)
    {
        throw new NotImplementedException();
    }

    public Task<HttpResponseWrapper<object>> UpdateApplicant(UpdateApplicantCommand request)
    {
        throw new NotImplementedException();
    }

    public Task<HttpResponseWrapper<object>> DeleteApplicant(string id)
    {
        throw new NotImplementedException();
    }

    public Task<HttpResponseWrapper<object>> FluentValidation(CreateApplicantCommand request)
    {
        throw new NotImplementedException();
    }

    public Task<HttpResponseWrapper<object>> GetApplicantNoAuth(GetApplicantForEditQuery request)
    {
        throw new NotImplementedException();
    }

    public Task<HttpResponseWrapper<object>> GetApplicantsNoAuth(GetApplicantsQuery request)
    {
        throw new NotImplementedException();
    }

    public Task<HttpResponseWrapper<object>> CreateApplicantNoAuth(CreateApplicantCommand request)
    {
        throw new NotImplementedException();
    }

    public Task<HttpResponseWrapper<object>> UpdateApplicantNoAuth(UpdateApplicantCommand request)
    {
        throw new NotImplementedException();
    }

    public Task<HttpResponseWrapper<object>> DeleteApplicantNoAuth(string id)
    {
        throw new NotImplementedException();
    }

    public Task<HttpResponseWrapper<object>> ExportAsPdf(ExportApplicantsQuery request)
    {
        throw new NotImplementedException();
    }
}
