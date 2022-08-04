using ElevateOTT.ClientPortal.Features.Content.Videos.Commands.CreateVideo;
using ElevateOTT.ClientPortal.Features.Content.Videos.Commands.UpdateVideo;
using ElevateOTT.ClientPortal.Features.Content.Videos.Queries.GetVideos;

namespace ElevateOTT.ClientPortal.Interfaces.Consumers;

public interface IVideosClient
{
    #region Public Methods

    Task<HttpResponseWrapper<object>> GetVideo(Guid id);
    Task<HttpResponseWrapper<object>> GetVideos(GetVideosQuery request);
    Task<HttpResponseWrapper<object>> CreateVideoFormData(MultipartFormDataContent request);
    Task<HttpResponseWrapper<object>> CreateVideo(CreateVideoCommand request);
    Task<HttpResponseWrapper<object>> UpdateVideoFormData(MultipartFormDataContent request);
    Task<HttpResponseWrapper<object>> UpdateVideo(UpdateVideoCommand request);
    Task<HttpResponseWrapper<object>> DeleteVideo(Guid id);

    #endregion Public Methods
}
