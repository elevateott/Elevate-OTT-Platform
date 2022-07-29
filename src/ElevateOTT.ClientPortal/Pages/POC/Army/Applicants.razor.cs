using ElevateOTT.ClientPortal.Pages.Users;
using Microsoft.Extensions.Options;

namespace ElevateOTT.ClientPortal.Pages.POC.Army;

public partial class Applicants : ComponentBase, IAsyncDisposable
{
    #region Private Properties

    public int ActivePanelIndex { get; set; } = 0;
    [Inject] private IAccessTokenProvider AccessTokenProvider { get; set; }
    [Inject] private IApiUrlProvider ApiUrlProvider { get; set; }
    [Inject] private IApplicantsClient ApplicantsClient { get; set; }
    [Inject] private IBreadcrumbService BreadcrumbService { get; set; }
    [Inject] private IDialogService DialogService { get; set; }
    [Inject] private IJSRuntime Js { get; set; }
    [Inject] private ILocalStorageService LocalStorage { get; set; }
    [Inject] private ISnackbar Snackbar { get; set; }
    [Inject] private AuthenticationStateProvider AuthenticationStateProvider { get; set; }
    [Inject] private NavigationManager NavigationManager { get; set; }

    private string SearchString { get; set; }
    private ApplicantsResponse ApplicantsResponse { get; set; }
    private ServerSideValidator ServerSideValidator { get; set; }
    private GetApplicantsQuery GetApplicantsQuery { get; set; } = new();
    private HubConnection HubConnection { get; set; }
    private MudTable<ApplicantItem> Table { get; set; }

    #endregion Private Properties

    #region Public Methods

    public async ValueTask DisposeAsync()
    {
        if (HubConnection is not null && HubConnection.State == HubConnectionState.Connected)
        {
            try
            {
                await HubConnection.StopAsync();
            }
            finally
            {
                await HubConnection.DisposeAsync();
                Snackbar.Add("Reporting Hub is closed.",Severity.Error);
            }
        }
    }

    #endregion Public Methods

    #region Protected Methods

    protected override async Task OnInitializedAsync()
    {
        BreadcrumbService.SetBreadcrumbItems(new List<BreadcrumbItem>
        {
            new(Resource.Home, "/"),
            new(Resource.Applicants, "#", true)
    });


        var userIdentity = (await AuthenticationStateProvider.GetAuthenticationStateAsync()).User.Identity;
        
        if (userIdentity is { IsAuthenticated: true })
        {
            await StartHubConnection();
            HubConnection.On("NotifyReportIssuer", (Func<FileMetaData, ReportStatus, Task>)(async
            (fileMetaData, reportStatus) =>
            {
                switch (reportStatus)
                {
                    case ReportStatus.Pending:
                        Snackbar.Add(Resource.Your_report_is_being_initiated, Severity.Info);
                        break;

                    case ReportStatus.InProgress:
                        Snackbar.Add(Resource.Your_report_is_being_generated,
                    Severity.Warning);
                        break;

                    case ReportStatus.Completed:
                        Snackbar.Add(
                    string.Format(Resource.Your_report_0_is_ready_to_download, fileMetaData.FileName),
                    Severity.Success);
                        await ShowDownloadFileDialogue(fileMetaData, reportStatus);
                        break;

                    case ReportStatus.Failed:
                        Snackbar.Add(Resource.Your_report_generation_has_failed,
                    Severity.Error);
                        break;

                    default:
                        throw new ArgumentOutOfRangeException(nameof(reportStatus), reportStatus, null);
                }
            }));
        }
    }

    #endregion Protected Methods

    #region Private Methods

    private void AddApplicant()
    {
        NavigationManager.NavigateTo("poc/army/addApplicant");
    }

    private void EditApplicant(string id)
    {
        NavigationManager.NavigateTo($"poc/army/editApplicant/{id}");
    }

    private async Task DeleteApplicant(string id)
    {
        var parameters = new DialogParameters
        {
            {"ContentText", Resource.Do_you_really_want_to_delete_this_record},
            {"ButtonText", Resource.Delete},
            {"Color", Color.Error}
        };

        var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

        var dialog = DialogService.Show<DialogModal>(Resource.Delete, parameters, options);

        var result = await dialog.Result;

        if (!result.Cancelled)
        {
            var httpResponseWrapper = await ApplicantsClient.DeleteApplicantNoAuth(id);

            if (httpResponseWrapper.Success)
            {
                var successResult = httpResponseWrapper.Response as SuccessResult<string>;
                Snackbar.Add(successResult.Result, Severity.Success);
                await Table.ReloadServerData();
            }
            else
            {
                var exceptionResult = httpResponseWrapper.Response as ExceptionResult;
                ServerSideValidator.Validate(exceptionResult);
            }
        }
    }

    private void FilterApplicants(string searchString)
    {
        SearchString = searchString;
        Table.ReloadServerData();
    }

    private async Task ImmediateExportApplicantToPdf()
    {
        var userIdentity = (await AuthenticationStateProvider.GetAuthenticationStateAsync()).User.Identity;

        if (userIdentity is { IsAuthenticated: true })
        {
            var parameters = new DialogParameters
            {
                {"ContentText", Resource.Exporting_data_may_take_a_while},
                {"ButtonText", Resource.ExportAsPdfImmediate},
                {"Color", Color.Error}
            };

            var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

            var dialog = DialogService.Show<DialogModal>(Resource.Export, parameters, options);

            var result = await dialog.Result;

            if (!result.Cancelled)
            {
                var httpResponseWrapper = await ApplicantsClient.ExportAsPdf(new ExportApplicantsQuery
                {
                    SearchText = GetApplicantsQuery.SearchText,
                    SortBy = GetApplicantsQuery.SortBy,
                    IsImmediate = true
                });

                if (httpResponseWrapper.Success)
                {
                    if (httpResponseWrapper.Response is SuccessResult<ExportApplicantsResponse> successResult)
                    {
                        Snackbar.Add(successResult.Result.FileUrl, Severity.Success);
                        await Js.InvokeVoidAsync("triggerFileDownload", successResult.Result.FileName, successResult.Result.FileUrl);
                    }
                }
                else
                {
                    var exceptionResult = httpResponseWrapper.Response as ExceptionResult;
                    ServerSideValidator.Validate(exceptionResult);
                }
            }
        }
        else
        {
            Snackbar.Add(Resource.You_are_not_authenticated_to_export_this_report, Severity.Warning);
        }
    }

    private async Task PostponedExportApplicantToPdf()
    {
        ActivePanelIndex = 0;

        var parameters = new DialogParameters
        {
            {"ContentText", Resource.Exporting_data_may_take_a_while},
            {"ButtonText", Resource.ExportAsPdfInBackground},
            {"Color", Color.Error}
        };

        var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

        var dialog = DialogService.Show<DialogModal>(Resource.Export, parameters, options);

        var result = await dialog.Result;

        if (!result.Cancelled)
        {
            var userIdentity = (await AuthenticationStateProvider.GetAuthenticationStateAsync()).User.Identity;

            if (userIdentity is { IsAuthenticated: true })
                await HubConnection.SendAsync("ExportApplicantToPdf", new ExportApplicantsQuery
                {
                    SearchText = GetApplicantsQuery.SearchText,
                    SortBy = GetApplicantsQuery.SortBy,
                    IsImmediate = false
                });
            else
                Snackbar.Add(Resource.You_are_not_authenticated_to_export_this_report, Severity.Warning);

            ActivePanelIndex = 1;
        }
    }

    private async Task<TableData<ApplicantItem>> ServerReload(TableState state)
    {
        GetApplicantsQuery.SearchText = SearchString;

        GetApplicantsQuery.PageNumber = state.Page + 1;

        GetApplicantsQuery.PageSize = state.PageSize;

        GetApplicantsQuery.SortBy = state.SortDirection == SortDirection.None ? string.Empty : $"{state.SortLabel} {state.SortDirection}";

        var responseWrapper = await ApplicantsClient.GetApplicants(GetApplicantsQuery);

        var tableData = new TableData<ApplicantItem>();

        if (responseWrapper.Success)
        {
            var successResult = responseWrapper.Response as SuccessResult<ApplicantsResponse>;
            if (successResult != null)
                ApplicantsResponse = successResult.Result;

            tableData = new TableData<ApplicantItem>()
            { TotalItems = ApplicantsResponse.Applicants.TotalRows, Items = ApplicantsResponse.Applicants.Items };
        }
        else
        {
            var exceptionResult = responseWrapper.Response as ExceptionResult;
            ServerSideValidator.Validate(exceptionResult);
        }

        return tableData;
    }

    private async Task ShowDownloadFileDialogue(FileMetaData fileMetaData, ReportStatus reportStatus)
    {
        var parameters = new DialogParameters
        {
            {"ContentText", Resource.Your_report_is_ready_to_download},
            {"ButtonText", Resource.Download},
            {"Color", Color.Error}
        };

        var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

        var dialog = DialogService.Show<DialogModal>(Resource.Export, parameters, options);

        var result = await dialog.Result;

        if (!result.Cancelled)
            await Js.InvokeVoidAsync("triggerFileDownload", fileMetaData.FileName, fileMetaData.FileUri);
    }

    private async Task StartHubConnection()
    {
        if (HubConnection is null || HubConnection.State == HubConnectionState.Disconnected)
        {
            Snackbar.Add("Reporting Hub is being initialed.", Severity.Info);

            var subDomain = NavigationManager.GetSubDomain();

            var culture = await LocalStorage.GetItemAsync<string>("Culture");

            HubConnection = new HubConnectionBuilder()
                .WithUrl($"{ApiUrlProvider.BaseHubUrl}/Hubs/DataExportHub?X-Tenant={subDomain}&Accept-Language={culture}",
                    options =>
                    {
                        //options.Headers.Add("X-Tenant", subDomain); //Doesn't Work
                        //options.Headers.Add("Accept-Language", culture); //Doesn't Work
                        options.AccessTokenProvider = () => AccessTokenProvider.TryGetAccessToken();
                    }).Build();

            await HubConnection.StartAsync();
            Snackbar.Add("Reporting Hub is now connected.", Severity.Success);
        }
    }

    private void ViewApplicant(string id)
    {
        NavigationManager.NavigateTo($"poc/army/viewApplicant/{id}");
    }

    #endregion Private Methods
}
