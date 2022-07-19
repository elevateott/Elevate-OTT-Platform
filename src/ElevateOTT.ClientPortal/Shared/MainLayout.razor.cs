namespace ElevateOTT.ClientPortal.Shared;

public partial class MainLayout
{
    #region Private Fields

    private readonly MudTheme _darkTheme = new()
    {
        Palette = new Palette()
        {
            Primary = "#776be7",
            Black = "#27272f",
            Background = "#32333d",
            BackgroundGrey = "#27272f",
            Surface = "#373740",
            DrawerBackground = "#27272f",
            DrawerText = "rgba(255,255,255, 0.50)",
            DrawerIcon = "rgba(255,255,255, 0.50)",
            AppbarBackground = "#27272f",
            AppbarText = "rgba(255,255,255, 0.70)",
            TextPrimary = "rgba(255,255,255, 0.70)",
            TextSecondary = "rgba(255,255,255, 0.50)",
            ActionDefault = "#adadb1",
            ActionDisabled = "rgba(255,255,255, 0.26)",
            ActionDisabledBackground = "rgba(255,255,255, 0.12)",
            Divider = "rgba(255,255,255, 0.12)",
            DividerLight = "rgba(255,255,255, 0.06)",
            TableLines = "rgba(255,255,255, 0.12)",
            LinesDefault = "rgba(255,255,255, 0.12)",
            LinesInputs = "rgba(255,255,255, 0.3)",
            TextDisabled = "rgba(255,255,255, 0.2)",
            Info = "#3299ff",
            Success = "#0bba83",
            Warning = "#ffa800",
            Error = "#f64e62",
            Dark = "#27272f"
        }
    };

    private readonly MudTheme _defaultTheme = new();
    private MudTheme _currentTheme = new();

    #endregion Private Fields

    #region Public Properties

    public bool DrawerOpen { get; set; } = true;

    //public bool IsDarkMode { get; set; }
    public bool IsDarkMode { get; set; } = true;

    public bool IsRightToLeft { get; set; }

    #endregion Public Properties

    #region Private Properties

    [Inject] private IAccessTokenProvider AccessTokenProvider { get; set; }
    [Inject] private IApiUrlProvider ApiUrlProvider { get; set; }
    [Inject] private AuthenticationStateProvider AuthenticationStateProvider { get; set; }
    [Inject] private IBreadcrumbService BreadcrumbService { get; set; }
    [Inject] private IDialogService DialogService { get; set; }
    [Inject] private HttpInterceptorService HttpInterceptorService { get; set; }
    [Inject] private IJSRuntime Js { get; set; }
    [Inject] private ILocalStorageService LocalStorage { get; set; }
    [Inject] private NavigationManager NavigationManager { get; set; }
    [Inject] private ISnackbar Snackbar { get; set; }

    private HubConnection HubConnection { get; set; }

    #endregion Private Properties

    //DO NOT REMOVE.

    #region Protected Methods

    protected override async Task OnInitializedAsync()
    {
        //_currentTheme = _defaultTheme;
        _currentTheme = _darkTheme;
        IsRightToLeft = !string.IsNullOrWhiteSpace(await LocalStorage.GetItemAsStringAsync("IsRightToLeft")) &&
                        Convert.ToBoolean(await LocalStorage.GetItemAsStringAsync("IsRightToLeft"));

        NavigationManager.LocationChanged += (obj, nav) => { StateHasChanged(); }; // To refresh the breadcrumb component

        Snackbar.Configuration.PositionClass = !IsRightToLeft ? Defaults.Classes.Position.TopRight : Defaults.Classes.Position.TopLeft;

        NavigationManager.LocationChanged += (obj, nav) => { StateHasChanged(); }; // To refresh the breadcrumb component

        //var userIdentity = (await AuthenticationStateProvider.GetAuthenticationStateAsync()).User.Identity;

        //if (userIdentity is { IsAuthenticated: true })
        //{
        //    await StartHubConnection();

        // HubConnection.On("NotifyReportIssuer", (Func<FileMetaData, ReportStatus, Task>)(async
        // (fileMetaData, reportStatus) => { switch (reportStatus) { case ReportStatus.Pending:
        // Snackbar.Add(Resource.Your_report_is_being_initiated, Severity.Info); break;

        // case ReportStatus.InProgress: Snackbar.Add(Resource.Your_report_is_being_generated,
        // Severity.Warning); break;

        // case ReportStatus.Completed: Snackbar.Add(
        // string.Format(Resource.Your_report_0_is_ready_to_download, fileMetaData.FileName),
        // Severity.Success); await ShowDownloadFileDialogue(fileMetaData, reportStatus); break;

        // case ReportStatus.Failed: Snackbar.Add(Resource.Your_report_generation_has_failed,
        // Severity.Error); break;

        //                default:
        //                    throw new ArgumentOutOfRangeException(nameof(reportStatus), reportStatus, null);
        //            }
        //        }));
        //}
    }

    #endregion Protected Methods

    #region Private Methods

    private void DarkMode()
    {
        if (_currentTheme == _defaultTheme)
        {
            _currentTheme = _darkTheme;
            IsDarkMode = true;
        }
        else
        {
            _currentTheme = _defaultTheme;
            IsDarkMode = false;
        }
    }

    private void DrawerToggle()
    {
        DrawerOpen = !DrawerOpen;
    }

    private async Task ShowDownloadFileDialogue(FileMetaData fileMetaData, ReportStatus reportStatus)
    {
        var parameters = new DialogParameters
        {
            {"ContentText", string.Format(Resource.Your_report_0_is_ready_to_download,fileMetaData.FileName)},
            {"ButtonText", Resource.Download},
            {"Color", Color.Error}
        };

        var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

        var dialog = DialogService.Show<DialogModal>(Resource.Download, parameters, options);

        var result = await dialog.Result;

        if (!result.Cancelled)
            await Js.InvokeVoidAsync("triggerFileDownload", fileMetaData.FileName, fileMetaData.FileUri);
    }

    //private void StartHubConnection()
    //{
    //    var subDomain = NavigationManager.GetSubDomain();

    //    var culture = await LocalStorage.GetItemAsync<string>("Culture");

    //    HubConnection = new HubConnectionBuilder()
    //        .WithUrl($"{ApiUrlProvider.BaseHubUrl}/Hubs/DataExportHub?X-Tenant={subDomain}&Accept-Language={culture}",
    //            options =>
    //            {
    //                //options.Headers.Add("X-Tenant", subDomain); //Doesn't Work
    //                //options.Headers.Add("Accept-Language", culture); //Doesn't Work
    //                options.AccessTokenProvider = () => AccessTokenProvider.TryGetAccessToken();
    //            }).Build();

    //    await HubConnection.StartAsync();
    //}

    #endregion Private Methods
}