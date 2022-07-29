using ElevateOTT.ClientPortal.Features.Content.Authors.Queries.GetAuthors;

namespace ElevateOTT.ClientPortal.Pages.Content.Authors;

public partial class Authors : ComponentBase, IAsyncDisposable
{
    #region Private Properties
    public int ActivePanelIndex { get; set; } = 0;
    [Inject] private IAccessTokenProvider? AccessTokenProvider { get; set; }
    [Inject] private IApiUrlProvider? ApiUrlProvider { get; set; }
    [Inject] private IAuthorsClient? AuthorsClient { get; set; }
    [Inject] private IBreadcrumbService? BreadcrumbService { get; set; }
    [Inject] private IDialogService? DialogService { get; set; }
    [Inject] private IJSRuntime? Js { get; set; }
    [Inject] private ILocalStorageService? LocalStorage { get; set; }
    [Inject] private ISnackbar? Snackbar { get; set; }
    [Inject] private AuthenticationStateProvider? AuthenticationStateProvider { get; set; }
    [Inject] private NavigationManager? NavigationManager { get; set; }

    private string SearchString { get; set; } = string.Empty;
    private AuthorsResponse? AuthorsResponse { get; set; }
    private ServerSideValidator? ServerSideValidator { get; set; }
    private GetAuthorsQuery GetAuthorsQuery { get; set; } = new();
    private HubConnection? HubConnection { get; set; }
    private MudTable<AuthorItem>? Table { get; set; }
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
                Snackbar?.Add("Reporting Hub is closed.", Severity.Error);
            }
        }
    }
    #endregion Public Methods

    #region Protected Methods
    protected override async Task OnInitializedAsync()
    {
        BreadcrumbService?.SetBreadcrumbItems(new List<BreadcrumbItem>
        {
            new(Resource.Home, "/"),
            new(Resource.Authors, "#", true)
        });

        var userIdentity = (await AuthenticationStateProvider.GetAuthenticationStateAsync()).User.Identity;

        if (userIdentity is { IsAuthenticated: true })
        {
            await StartHubConnection();
            HubConnection?.On("NotifyReportIssuer", (Func<FileMetaData, ReportStatus, Task>)(async
            (fileMetaData, reportStatus) =>
            {
                switch (reportStatus)
                {
                    case ReportStatus.Pending:
                        Snackbar?.Add(Resource.Your_report_is_being_initiated, Severity.Info);
                        break;

                    case ReportStatus.InProgress:
                        Snackbar?.Add(Resource.Your_report_is_being_generated,
                    Severity.Warning);
                        break;

                    case ReportStatus.Completed:
                        Snackbar?.Add(
                    string.Format(Resource.Your_report_0_is_ready_to_download, fileMetaData.FileName),
                    Severity.Success);
                        await ShowDownloadFileDialogue(fileMetaData, reportStatus);
                        break;

                    case ReportStatus.Failed:
                        Snackbar?.Add(Resource.Your_report_generation_has_failed,
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
    private void AddAuthor()
    {
        NavigationManager?.NavigateTo("poc/army/addAuthor");
    }

    private void EditAuthor(Guid id)
    {
        NavigationManager?.NavigateTo($"poc/army/editAuthor/{id}");
    }

    private async Task DeleteAuthor(Guid id)
    {
        var parameters = new DialogParameters
        {
            {"ContentText", Resource.Do_you_really_want_to_delete_this_record},
            {"ButtonText", Resource.Delete},
            {"Color", Color.Error}
        };

        var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

        var dialog = DialogService?.Show<DialogModal>(Resource.Delete, parameters, options);

        var result = await dialog.Result;

        if (!result.Cancelled)
        {
            var httpResponseWrapper = await AuthorsClient.DeleteAuthor(id);

            if (httpResponseWrapper.Success)
            {
                var successResult = httpResponseWrapper.Response as SuccessResult<string>;
                Snackbar?.Add(successResult?.Result, Severity.Success);
                await Table.ReloadServerData();
            }
            else
            {
                var exceptionResult = httpResponseWrapper.Response as ExceptionResult;
                ServerSideValidator?.Validate(exceptionResult);
            }
        }
    }

    private void FilterAuthors(string searchString)
    {
        SearchString = searchString;
        Table?.ReloadServerData();
    }

    private async Task<TableData<AuthorItem>> ServerReload(TableState state)
    {
        // TODO guard clause

        GetAuthorsQuery.SearchText = SearchString;

        GetAuthorsQuery.PageNumber = state.Page + 1;

        GetAuthorsQuery.PageSize = state.PageSize;

        GetAuthorsQuery.SortBy = state.SortDirection == SortDirection.None ? string.Empty : $"{state.SortLabel} {state.SortDirection}";

        var responseWrapper = await AuthorsClient.GetAuthors(GetAuthorsQuery);

        var tableData = new TableData<AuthorItem>();

        if (responseWrapper.Success)
        {
            var successResult = responseWrapper.Response as SuccessResult<AuthorsResponse>;
            if (successResult != null)
                AuthorsResponse = successResult.Result;

            tableData = new TableData<AuthorItem>()
            { TotalItems = AuthorsResponse.Authors.TotalRows, Items = AuthorsResponse.Authors.Items };
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

    private void ViewAuthor(Guid id)
    {
        NavigationManager.NavigateTo($"poc/army/viewAuthor/{id}");
    }
    #endregion Private Methods
}
