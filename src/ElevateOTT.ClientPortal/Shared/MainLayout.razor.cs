using ElevateOTT.ClientPortal.Theme;

namespace ElevateOTT.ClientPortal.Shared;

public partial class MainLayout
{
    #region Private Fields

    private readonly MudTheme _defaultTheme = new AppLightModeTheme();
    private readonly MudTheme _darkTheme = new AppDarkModeTheme();
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
        _currentTheme = _defaultTheme;
        //_currentTheme = _darkTheme;
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
