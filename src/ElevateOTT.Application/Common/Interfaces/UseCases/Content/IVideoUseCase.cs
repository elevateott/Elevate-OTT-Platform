using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElevateOTT.Application.Features.Content.Videos.Commands.CreateVideo;
using ElevateOTT.Application.Features.Content.Videos.Commands.DeleteVideo;
using ElevateOTT.Application.Features.Content.Videos.Commands.UpdateVideo;
using ElevateOTT.Application.Features.Content.Videos.Queries.ExportVideos;
using ElevateOTT.Application.Features.Content.Videos.Queries.GetVideoForEdit;
using ElevateOTT.Application.Features.Content.Videos.Queries.GetVideos;

namespace ElevateOTT.Application.Common.Interfaces.UseCases.Content;
public interface IVideoUseCase
{
    #region Public Methods

    Task<Envelope<VideoForEdit>> GetVideo(GetVideoForEditQuery request);
    Task<Envelope<VideosResponse>> GetVideos(GetVideosQuery request);
    Task<Envelope<CreateVideoResponse>> AddVideo(CreateVideoCommand request);
    Task<Envelope<string>> EditVideo(UpdateVideoCommand request);
    Task<Envelope<string>> DeleteVideo(DeleteVideoCommand request);
    Task<Envelope<ExportVideosResponse>> ExportAsPdf(ExportVideosQuery request);
    
    #endregion Public Methods
}
