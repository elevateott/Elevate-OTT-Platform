using ElevateOTT.ClientPortal.Features.Content.Videos.Commands.CreateVideo;
using ElevateOTT.ClientPortal.Features.Content.Videos.Commands.UpdateVideo;
using ElevateOTT.ClientPortal.Features.Content.Videos.Queries.GetVideos;
using ElevateOTT.ClientPortal.Models.Videos;

namespace ElevateOTT.ClientPortal.Interfaces.Consumers;

public interface IVideosClient
{
    #region Public Methods

    Task<HttpResponseWrapper<object>> GetVideo(Guid id);
    Task<HttpResponseWrapper<object>> GetVideos(GetVideosQuery request);
    Task<HttpResponseWrapper<object>> GetAzureBlobSasToken();
    Task<HttpResponseWrapper<object>> GetNewStorageName();
    Task DirectUploadToAzureStorageAsync(Uri uriSasToken, UploadFileModel file,
        CancellationToken cancellationToken = default);
    //Task StoreVideosForStreaming();

    //Task<HttpResponseWrapper<object>> CreateVideo(MultipartFormDataContent request);
    Task<HttpResponseWrapper<object>> CreateVideo(CreateVideoCommand request);
    Task<HttpResponseWrapper<object>> UpdateVideo(MultipartFormDataContent request);
    //Task<HttpResponseWrapper<object>> UpdateVideo(UpdateVideoCommand request);
    Task<HttpResponseWrapper<object>> DeleteVideo(Guid id);

    #endregion Public Methods
}
