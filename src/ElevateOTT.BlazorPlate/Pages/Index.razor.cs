namespace ElevateOTT.BlazorPlate.Pages;

public partial class Index : IAsyncDisposable
{
    #region Private Properties

    [Inject] private NavigationManager NavigationManager { get; set; }
    [Inject] private IBreadcrumbService BreadcrumbService { get; set; }
    [Inject] private IDashboardClient DashboardClient { get; set; }
    [Inject] private IApiUrlProvider ApiUrlProvider { get; set; }
    [Inject] private IAccessTokenProvider AccessTokenProvider { get; set; }
    [Inject] private ILocalStorageService LocalStorage { get; set; }
    [Inject] private ISnackbar Snackbar { get; set; }

    private HubConnection HubConnection { get; set; }
    private ServerSideValidator ServerSideValidator { get; set; } = new();
    private HeadlinesResponse HeadlinesResponse { get; set; } = new();

    #endregion Private Properties

    #region Public Methods

    public async Task InitiateSignalRHub()
    {
        Snackbar.Add("Dashboard Hub is being initialed.", Severity.Info);

        var httpResponseWrapper = await DashboardClient.GetHeadlinesData();

        if (httpResponseWrapper.Response is SuccessResult<HeadlinesResponse> successResult)
            HeadlinesResponse = successResult.Result;

        if (httpResponseWrapper.Success)
        {
            await StartHubConnection();
            HubConnection.On<HeadlinesResponse>("SendHeadlinesData", (data) =>
            {
                HeadlinesResponse = data;
                StateHasChanged();
            });
        }
        else
        {
            var exceptionResult = httpResponseWrapper.Response as ExceptionResult;
            ServerSideValidator.Validate(exceptionResult);
        }
    }

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
                Snackbar.Add("Dashboard Hub is closed.", Severity.Error);
            }
        }
    }

    public async void CloseHubConnection()
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
                Snackbar.Add("Dashboard Hub is closed.", Severity.Error);
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
            new(Resource.Dashboard, "#", true)
        });

        await InitiateSignalRHub();
    }

    #endregion Protected Methods

    #region Private Methods

    private async Task StartHubConnection()
    {
        if (HubConnection is null || HubConnection.State == HubConnectionState.Disconnected)
        {
            var subDomain = NavigationManager.GetSubDomain();

            var culture = await LocalStorage.GetItemAsync<string>("Culture");

            HubConnection = new HubConnectionBuilder()
                .WithUrl($"{ApiUrlProvider.BaseHubUrl}/Hubs/DashboardHub?X-Tenant={subDomain}&Accept-Language={culture}",
                    options =>
                    {
                        //options.Headers.Add("X-Tenant", subDomain); //Doesn't Work
                        options.AccessTokenProvider = () => AccessTokenProvider.TryGetAccessToken();
                    }).Build();

            await HubConnection.StartAsync();

            Snackbar.Add("Dashboard Hub is now connected.", Severity.Success);
        }
    }

    #endregion Private Methods
}