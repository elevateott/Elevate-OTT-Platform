using ElevateOTT.StreamingWebApp.Features.POC.Applicants.Queries.GetApplicantForEdit;
using ElevateOTT.StreamingWebApp.Interfaces.Consumers;
using ElevateOTT.StreamingWebApp.Models;
using ElevateOTT.StreamingWebApp.Services;
using ElevateOTT.StreamingWebApp.Shared;

namespace ElevateOTT.StreamingWebApp.Pages.POC.Army;

public partial class ViewApplicant
{
    #region Public Properties

    [Parameter] public string ApplicantId { get; set; }

    #endregion Public Properties

    #region Private Properties

    [Inject] private IBreadcrumbService BreadcrumbService { get; set; }
    [Inject] private IApplicantsClient ApplicantsClient { get; set; }

    private ServerSideValidator ServerSideValidator { get; set; }
    private ApplicantForEdit ApplicantForEditVm { get; set; } = new();

    #endregion Private Properties

    #region Protected Methods

    protected override async Task OnInitializedAsync()
    {
        BreadcrumbService.SetBreadcrumbItems(new List<BreadcrumbItem>
        {
            new(Resource.Home, "/"),
            new(Resource.Applicants, "/poc/army/applicants"),
            new(Resource.View_Applicant, "#", true)
        });

        var httpResponseWrapper = await ApplicantsClient.GetApplicant(new GetApplicantForEditQuery
        {
            Id = ApplicantId,
        });

        if (httpResponseWrapper.Success)
        {
            var successResult = httpResponseWrapper.Response as SuccessResult<ApplicantForEdit>;
            ApplicantForEditVm = successResult?.Result;
        }
        else
        {
            var exceptionResult = httpResponseWrapper.Response as ExceptionResult;
            ServerSideValidator.Validate(exceptionResult);
        }
    }

    #endregion Protected Methods
}
