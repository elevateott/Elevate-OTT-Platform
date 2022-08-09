using ElevateOTT.ClientPortal.Features.Content.Videos.Queries.GetVideos;
using ElevateOTT.ClientPortal.Hubs;

namespace ElevateOTT.ClientPortal.Pages.Content.Videos;

    public partial class VideoList : ComponentBase, IAsyncDisposable
    {
        #region Private Properties
        [Inject] private IAccessTokenProvider? AccessTokenProvider { get; set; }
        [Inject] private IApiUrlProvider? ApiUrlProvider { get; set; }
        [Inject] private IVideosClient? VideosClient { get; set; }
        [Inject] private IDialogService? DialogService { get; set; }
        [Inject] private IJSRuntime? Js { get; set; }
        [Inject] private ILocalStorageService? LocalStorage { get; set; }
        [Inject] private ISnackbar? Snackbar { get; set; }
        [Inject] private AuthenticationStateProvider? AuthenticationStateProvider { get; set; }
        [Inject] private NavigationManager? NavigationManager { get; set; }
        [Inject] public VideoHub? VideoHub { get; set; }

        private string SearchString { get; set; } = string.Empty;
        private VideosResponse? VideosResponse { get; set; }
        private ServerSideValidator? ServerSideValidator { get; set; }
        private GetVideosQuery GetVideosQuery { get; set; } = new();
        private HubConnection? HubConnection { get; set; }

        private string _message;
        private bool _updateReceived;

        private MudTable<VideoItem>? Table { get; set; }
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

            if (VideoHub?.HubConnection is not null && VideoHub.HubConnection.State == HubConnectionState.Connected)
            {
                try
                {
                    await VideoHub.HubConnection.StopAsync();
                }
                finally
                {
                    await VideoHub.HubConnection.DisposeAsync();
                    Snackbar?.Add("Video Hub is closed.", Severity.Error);
                }
            }
        }

        #endregion Public Methods

        #region Protected Methods
        protected override async Task OnInitializedAsync()
        {
            
            // TODO connect to video hub

            var userIdentity = (await AuthenticationStateProvider.GetAuthenticationStateAsync()).User.Identity;

            if (userIdentity is { IsAuthenticated: true })
            {
                //await StartHubConnection();
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

            //await ConnectToVideoHub();
        }
        #endregion Protected Methods

        #region Private Methods

        private async Task Connect()
        {
            await ConnectToVideoHub();

        }

        private async Task ConnectToVideoHub()
        {

            Console.WriteLine("Connecting To VideoHub");

            VideoHub?.Connect();
            //if (VideoHub?.HubConnection == null) return;

            //VideoHub.HubConnection.On<string>("ReceiveVideoUpdate", (message) =>
            //{
            //    Console.WriteLine($"ReceiveVideoUpdate message: {message}");
            //    _message = message;
            //    _updateReceived = true;
            //    InvokeAsync(StateHasChanged);
            //});

        VideoHub.HubConnection.On<Guid?, AssetCreationStatus>("ReceiveVideoUpdate", (videoId, status) =>
        {
                // TODO do stuff with return data

            Console.WriteLine("video hub update received!");
            Console.WriteLine($"video id: {videoId}");
            Console.WriteLine($"status: {status}");

            _updateReceived = true;
            InvokeAsync(StateHasChanged);
        });

        await VideoHub.HubConnection.StartAsync();
    }

        private void AddVideo()
            {
                NavigationManager?.NavigateTo("poc/army/addVideo");
            }

        private void EditVideo(Guid id)
        {
            NavigationManager?.NavigateTo($"/content/video/{id}");
        }

        private async Task DeleteVideo(Guid id)
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
                var httpResponseWrapper = await VideosClient.DeleteVideo(id);

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

        private void FilterVideos(string searchString)
        {
            SearchString = searchString;
            Table?.ReloadServerData();
        }

        private async Task<TableData<VideoItem>> ServerReload(TableState state)
        {
            // TODO guard clause

            GetVideosQuery.SearchText = SearchString;

            GetVideosQuery.PageNumber = state.Page + 1;

            GetVideosQuery.PageSize = state.PageSize;

            GetVideosQuery.SortBy = state.SortDirection == SortDirection.None ? string.Empty : $"{state.SortLabel} {state.SortDirection}";

            Console.WriteLine("Server reload pre api call");

            var responseWrapper = await VideosClient.GetVideos(GetVideosQuery);

            Console.WriteLine("Server reload post api call");

            var tableData = new TableData<VideoItem>();

            if (responseWrapper.Success)
            {
                var successResult = responseWrapper.Response as SuccessResult<VideosResponse>;
                if (successResult != null)
                    VideosResponse = successResult.Result;

                tableData = new TableData<VideoItem>()
                { TotalItems = VideosResponse.Videos.TotalRows, Items = VideosResponse.Videos.Items };
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

        //private async Task StartVideoHubConnection()
        //{
        //    if (VideoHub?.HubConnection is null || VideoHub.HubConnection.State == HubConnectionState.Disconnected)
        //    {
        //        Snackbar.Add("Video Hub is being initialed.", Severity.Info);

        //        var subDomain = NavigationManager.GetSubDomain();

        //        var culture = await LocalStorage.GetItemAsync<string>("Culture");

        //        HubConnection = new HubConnectionBuilder()
        //            .WithUrl($"{ApiUrlProvider.BaseHubUrl}/Hubs/VideoHub?X-Tenant={subDomain}&Accept-Language={culture}",
        //                options =>
        //                {
        //                    //options.Headers.Add("X-Tenant", subDomain); //Doesn't Work
        //                    //options.Headers.Add("Accept-Language", culture); //Doesn't Work
        //                    options.AccessTokenProvider = () => AccessTokenProvider.TryGetAccessToken();
        //                }).Build();

        //        await HubConnection.StartAsync();
        //        Snackbar.Add("Video Hub is now connected.", Severity.Success);
        //    }
        //}

        private void ViewVideo(Guid id)
            {
                NavigationManager.NavigateTo($"poc/army/viewVideo/{id}");
            }
        #endregion Private Methods
    }

