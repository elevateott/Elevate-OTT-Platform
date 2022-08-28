﻿using System.ComponentModel;
using ElevateOTT.ClientPortal.Features.Content.Videos.Commands.CreateVideo;
using ElevateOTT.ClientPortal.Features.Content.Videos.Queries.GetNewStorageName;
using ElevateOTT.ClientPortal.Features.Content.Videos.Queries.GetSasToken;
using ElevateOTT.ClientPortal.Models.Videos;

namespace ElevateOTT.ClientPortal.Pages.Content.Videos;

public partial class VideoUpload : ComponentBase, IDisposable
{
    [Inject] private ISnackbar? Snackbar { get; set; }
    [Inject] private IVideosClient? VideosClient { get; set; }
    [Inject] private NavigationManager? NavigationManager { get; set; }

    [Parameter]
    public EventCallback OnVideoUploadComplete { get; set; }

    private SasTokenResponse? SasTokenResponse { get; set; }
    private NewStorageNameResponse? NewStorageNameResponse { get; set; }
    private ServerSideValidator? ServerSideValidator { get; set; }

    private List<UploadFileModel>? _filesToUpload = new ();

    private string _acceptedFileExtensions = ".mp4, .mkv, .mov, .avi, .wmv";

    private int _maximumFileCount = 12;

    private bool _uploadInProgress = false;

    private bool _showMaxNumberReachedAlert = false;

    private bool _clearing = false;
    private static string _defaultDragClass = "relative rounded-lg border-2 border-dashed pa-4 mt-4 mud-width-full mud-height-full";
    private string _dragClass = _defaultDragClass;

    private CancellationTokenSource? _cts;

    private UploadProgressModel? _uploadProgress;

    IReadOnlyList<IBrowserFile>? _files = null;


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
        _clearing = true;
        _filesToUpload?.Clear();
        ClearDragClass();
        await Task.Delay(100);
        _clearing = false;
        _showMaxNumberReachedAlert = false;
    }
    private async Task UploadAsync()
    {
        if (VideosClient is null) return;

        // TODO diable upload field

        // get SAS token from Azure via API
        var responseWrapper = await VideosClient.GetAzureBlobSasToken();


        //
        // TODO refactor this
        //
        if (responseWrapper.Success)
        {
            var successResult = responseWrapper.Response as SuccessResult<SasTokenResponse>;
            if (successResult != null)
                SasTokenResponse = successResult.Result;
        }
        else
        {
            var exceptionResult = responseWrapper.Response as ExceptionResult;
            ServerSideValidator?.Validate(exceptionResult);
        }


        _uploadInProgress = true;

        System.Console.WriteLine("_files count: " + _files?.Count());

        Console.WriteLine($"SasTokenResponse : {SasTokenResponse}");

        _cts = new CancellationTokenSource();

        // string blobBaseUrl = sasResult.ContainerUri.AbsolutePath;
        if (SasTokenResponse != null)
        {
            string blobContainerUrl = SasTokenResponse.ContainerUri.AbsoluteUri;
            string sasToken = SasTokenResponse.SASToken;

            System.Console.WriteLine("_filesToUpload count: " + _filesToUpload?.Count());

            System.Console.WriteLine($"blobContainerUrl: {blobContainerUrl}");
            System.Console.WriteLine($"sasToken: {sasToken}");

            if (_filesToUpload is not null)
            {
                foreach (var file in _filesToUpload)
                {

                    // TODO refactor

                    responseWrapper = await VideosClient.GetNewStorageName();

                    if (responseWrapper.Success)
                    {
                        var successResult = responseWrapper.Response as SuccessResult<NewStorageNameResponse>;
                        if (successResult != null)
                            NewStorageNameResponse = successResult.Result;
                    }
                    else
                    {
                        var exceptionResult = responseWrapper.Response as ExceptionResult;
                        ServerSideValidator?.Validate(exceptionResult);
                        continue;
                    }
                    
                    file.StorageName = $"{NewStorageNameResponse?.Name}{file.Extension.ToLower()}";
                    string directUploadUrl = $"{blobContainerUrl}/{file.StorageName}{sasToken}";

                    System.Console.WriteLine($"url: {directUploadUrl}");
                    Console.WriteLine($"NewStorageNameResponse : {NewStorageNameResponse}");


                    file.UploadProgress = UploadProgressModel.CreateUploadProgress();
                    file.UploadProgress.Maximum = 100;
                    file.UploadProgress.PropertyChanged += ProgressValueChangedHandler;

                    Uri sasUri = new Uri(directUploadUrl);

                    Console.WriteLine($"sas uri: {sasUri}");



                    await VideosClient.DirectUploadToAzureStorageAsync(sasUri, file, _cts.Token);
                    //
                    //  save to db
                    //
                    var createVideoCommand = new CreateVideoCommand()
                    {
                        FileName = file.FileName,
                        BlobName = file.StorageName,
                        StreamCreationStatus = AssetCreationStatus.None
                    };

                    var createVideoResponse = await VideosClient.CreateVideo(createVideoCommand);

                    Console.WriteLine("httpResponse: " + createVideoResponse);
                    Console.WriteLine("httpResponse.Success: " + createVideoResponse.Success);


                    if (createVideoResponse.Success)
                    {
                        var successResult = createVideoResponse.Response as SuccessResult<string>;
                        Snackbar?.Add(successResult?.Result, Severity.Success);
                        Console.WriteLine("OnVideoUploadComplete success");
                        await OnVideoUploadComplete.InvokeAsync();
                    }
                    else
                    {
                        var exceptionResult = createVideoResponse.Response as ExceptionResult;
                        //_editContextServerSideValidator?.Validate(exceptionResult);
                        //_serverSideValidator?.Validate(exceptionResult);
                    }
                }
            }
        }

        _filesToUpload?.Clear();
        _uploadInProgress = false;

        ShowSnackbar("Upload complete!", Severity.Success);
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
        _dragClass = $"{_defaultDragClass} mud-border-primary";
    }

    private void ClearDragClass()
    {
        _dragClass = _defaultDragClass;
    }

    private string StripHyphensFromGuid(Guid guid)
    {
        return guid.ToString().Replace("-", "");
    }

    public void Dispose()
    {
        _cts?.Dispose();
        Snackbar?.Dispose();
    }
}

