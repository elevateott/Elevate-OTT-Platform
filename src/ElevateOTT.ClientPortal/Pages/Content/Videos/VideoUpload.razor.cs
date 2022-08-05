using static System.Net.WebRequestMethods;
using System.Net.NetworkInformation;

using ElevateOTT.Shared.DataTransferObjects.Videos;
using ElevateOTT.Shared.Models ;
using HttpRepository.Contracts;
    
using Syncfusion.Blazor.Inputs;
using System.ComponentModel;

namespace ElevateOTT.ClientPortal.Pages.Content.Videos;

public partial class VideoUpload : IDisposable
{
    // inject ISnackbar Snackbar;


    [Inject]
    public IVideoHttpRepository? _videoRepository { get; set; }

    [Inject]
    public IUploadHttpRepository? _uploadRepository { get; set; }

    private List<UploadFileModel>? _filesToUpload = new List<UploadFileModel>();

    private string _acceptedFileExtensions = ".mp4, .mkv, .mov, .avi, .wmv";

    private int _maximumFileCount = 12;

    private bool _uploadInProgress = false;

    private bool _showMaxNumberReachedAlert = false;

    private bool Clearing = false;
    private static string DefaultDragClass = "relative rounded-lg border-2 border-dashed pa-4 mt-4 mud-width-full mud-height-full";
    private string DragClass = DefaultDragClass;

    private CancellationTokenSource? _cts;

    private UploadProgressModel? _uploadProgress;

    IReadOnlyList<IBrowserFile>? _files = null;

    protected override async Task OnInitializedAsync()
    {
    }

    private void OnInputFileChanged(InputFileChangeEventArgs e)
    {
        _showMaxNumberReachedAlert = false;

        ClearDragClass();

        // TODO handle error if too many files added at once
        try
        {
            _files = e.GetMultipleFiles(maximumFileCount: _maximumFileCount);
        }
        catch (InvalidOperationException ex)
        {
            System.Console.WriteLine(ex.Message);
            _showMaxNumberReachedAlert = true;
        }

        System.Console.WriteLine("_files count: " + _files?.Count());

        if (_files is null) return;

        if (_files.Count() > _maximumFileCount)
        {
            _showMaxNumberReachedAlert = true;
            StateHasChanged();
        }

        System.Console.WriteLine($"_currentTenantId.Value: {_currentTenantId.Value}");

        foreach (var file in _files)
        {
            if (_filesToUpload?.Count() < _maximumFileCount)
            {
                _filesToUpload?.Add(
                    new UploadFileModel
                    {
                        BrowserFile = file,
                        FileName = file.Name,
                        FileSize = file.Size,
                        Extension = Path.GetExtension(file.Name),
                        ContentType = file.ContentType,
                        MaxSizeAllowed = 10737418240, // 10GB default
                    }
                );
            }
            else
            {
                _showMaxNumberReachedAlert = true;
            }
        }
    }

    private async Task Clear()
    {
        Clearing = true;
        _filesToUpload?.Clear();
        ClearDragClass();
        await Task.Delay(100);
        Clearing = false;
        _showMaxNumberReachedAlert = false;
    }
    private async Task UploadAsync()
    {
        if (_uploadRepository is null || _videoRepository is null || _currentTenantId is null) return;

        // TODO diable upload field

        // get SAS token
        var sasResult = await _uploadRepository.GetSASTokenFromAzure(_currentTenantId.Value);

        // TODO handle null result
        if (sasResult is null) return;

        _uploadInProgress = true;

        System.Console.WriteLine("_files count: " + _files?.Count());

        _cts = new CancellationTokenSource();

        // string blobBaseUrl = sasResult.ContainerUri.AbsolutePath;
        string blobContainerUrl = sasResult.ContainerUri.AbsoluteUri;
        string sasToken = sasResult.SASToken;

        System.Console.WriteLine("_filesToUpload count: " + _filesToUpload?.Count());

        System.Console.WriteLine($"blobContainerUrl: {blobContainerUrl}");
        System.Console.WriteLine($"sasToken: {sasToken}");

        if (_filesToUpload is not null)
        {
            foreach (var file in _filesToUpload)
            {
                // generates storage name on backend
                string? generatedFileName = await _videoRepository.GenerateStorageName(_currentTenantId.Value);

                // backup plan for storage name in case failure
                if (generatedFileName is null) generatedFileName = $"{StripHyphensFromGuid(Guid.NewGuid())}";
                file.StorageName = $"{generatedFileName}{file.Extension.ToLower()}";

                string directUploadUrl = $"{blobContainerUrl}/{file.StorageName}{sasToken}";
                System.Console.WriteLine($"url: {directUploadUrl}");

                file.UploadProgress = UploadProgressModel.CreateUploadProgress();
                file.UploadProgress.Maximum = 100;
                file.UploadProgress.PropertyChanged += ProgressValueChangedHandler;

                Uri sasUri = new Uri(directUploadUrl);
                await _uploadRepository.DirectUploadToAzureStorageAsync(sasUri, file, _cts.Token);
                //
                //  save to db
                //
                var videoCreation = new VideoForCreationDto
                {
                    FileName = file.FileName,
                    BlobName = file.StorageName,
                    StreamCreationStatus = ElevateOTT.Shared.AssetCreationStatus.None
                };

                await _videoRepository.CreateVideoAsync(_currentTenantId.Value, videoCreation);
            }
        }
        _filesToUpload?.Clear();
        _uploadInProgress = false;

        ShowSnackbar("Upload complete!", Severity.Success);

        // store video assets for streaming
        await _videoRepository.StoreVideosForStreaming(_currentTenantId.Value);
    }

    private void ShowSnackbar(string message, Severity level)
    {
        Snackbar.Configuration.PositionClass = Defaults.Classes.Position.TopCenter;
        Snackbar.Add(message, level);
    }

    private void ProgressValueChangedHandler(object? sender, PropertyChangedEventArgs e)
    {
        StateHasChanged();
    }

    private void CancelUpload()
    {
        _cts?.Cancel();
    }

    private void SetDragClass()
    {
        DragClass = $"{DefaultDragClass} mud-border-primary";
    }

    private void ClearDragClass()
    {
        DragClass = DefaultDragClass;
    }

    private string StripHyphensFromGuid(Guid guid)
    {
        return guid.ToString().Replace("-", "");
    }

    public override void Dispose()
    {
        _cts?.Dispose();
    }
}

