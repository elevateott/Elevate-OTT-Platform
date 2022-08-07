using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs.Specialized;
using ElevateOTT.ClientPortal.Features.Content.Videos.Commands.CreateVideo;
using ElevateOTT.ClientPortal.Features.Content.Videos.Commands.UpdateVideo;
using ElevateOTT.ClientPortal.Features.Content.Videos.Queries.GetNewStorageName;
using ElevateOTT.ClientPortal.Features.Content.Videos.Queries.GetSasToken;
using ElevateOTT.ClientPortal.Features.Content.Videos.Queries.GetVideoForEdit;
using ElevateOTT.ClientPortal.Features.Content.Videos.Queries.GetVideos;
using ElevateOTT.ClientPortal.Models.Videos;

namespace ElevateOTT.ClientPortal.Consumers;

public class VideosClient : IVideosClient
{
    #region Private Fields

    private readonly IHttpService _httpService;
    private const string ControllerName = "videos";

    #endregion Private Fields

    #region Public Constructors

    public VideosClient(IHttpService httpService)
    {
        _httpService = httpService;
    }

    #endregion Public Constructors

    public async Task<HttpResponseWrapper<object>> GetVideo(Guid id)
    {
        return await _httpService.Get<VideoForEdit>($"{ControllerName}/{id}");
    }

    public async Task<HttpResponseWrapper<object>> GetAzureBlobSasToken()
    {
        return await _httpService.Get<SasTokenResponse>($"{ControllerName}/azure-blob-sas-token");
    }

    public async Task<HttpResponseWrapper<object>> GetNewStorageName()
    {
        return await _httpService.Get<NewStorageNameResponse>($"{ControllerName}/new-storage-name");
    }

    public async Task<HttpResponseWrapper<object>> GetVideos(GetVideosQuery request)
    {
        return await _httpService.Post<GetVideosQuery, VideosResponse>($"{ControllerName}", request);
    }

    //public async Task<HttpResponseWrapper<object>> CreateVideo(MultipartFormDataContent request)
    //{
    //    return await _httpService.PostFormData<MultipartFormDataContent, CreateVideoResponse>($"{ControllerName}", request);
    //}

    //public async Task StoreVideosForStreaming()
    //{
    //    await _httpService.Get($"{ControllerName}/new-storage-name");
    //}

    public async Task<HttpResponseWrapper<object>> CreateVideo(CreateVideoCommand request)
    {
        return await _httpService.Post<CreateVideoCommand, CreateVideoResponse>($"{ControllerName}/add", request);
    }

    public async Task DirectUploadToAzureStorageAsync(Uri uriSasToken, UploadFileModel file, CancellationToken cancellationToken = default)
    {
        BlockBlobClient blockBlobClient = new BlockBlobClient(uriSasToken);

        Console.WriteLine($"name: {blockBlobClient.Name}");
        Console.WriteLine($"container name: {blockBlobClient.BlobContainerName}");
        Console.WriteLine($"file size: {file.FileSize}");

        await using var stream = file.BrowserFile?.OpenReadStream(file.MaxSizeAllowed, cancellationToken);

        var uploadOptions = new BlobUploadOptions
        {
            HttpHeaders = new BlobHttpHeaders
            {
                ContentType = file.ContentType
            },
            ProgressHandler = new Progress<long>(p =>
            {
                if (file.UploadProgress is null) return;
                file.UploadProgress.Value = Convert.ToDouble(p * 100 / file.FileSize);
                Console.WriteLine($"progress: {file.UploadProgress.Value}");
            })
        };

        await blockBlobClient.UploadAsync(stream, uploadOptions, cancellationToken);
    }
    

    public async Task<HttpResponseWrapper<object>> UpdateVideo(MultipartFormDataContent request)
    {
        Console.WriteLine("UpdateVideo invoked");
        Console.WriteLine("request: " + request);
        return await _httpService.PostFormData<MultipartFormDataContent, string>($"{ControllerName}", request);
    }

    //public async Task<HttpResponseWrapper<object>> UpdateVideo(UpdateVideoCommand request)
    //{
    //    return await _httpService.Put<UpdateVideoCommand, string>("videos", request);
    //}

    public async Task<HttpResponseWrapper<object>> DeleteVideo(Guid id)
    {
        return await _httpService.Delete<string>($"{ControllerName}/{id}");
    }
}
