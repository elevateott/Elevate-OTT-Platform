using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Spreadsheet;
using ElevateOTT.Application.Common.Interfaces.UseCases.Content;
using ElevateOTT.Application.Features.Content.Authors.Queries.GetAuthors;
using ElevateOTT.Application.Features.Content.Authors.Queries.GetAuthorsForAutoComplete;
using ElevateOTT.Application.Features.Content.Videos.Queries.GetVideos;

namespace ElevateOTT.Application.Features.Content.Videos.Queries.GetVideosForAutoComplete;

public class GetVideosForAutoCompleteQuery : FilterableQuery, IRequest<Envelope<VideosForAutoCompleteResponse>>
{
    #region Public Classes
    public class GetVideosForAutoCompleteQueryHandler : IRequestHandler<GetVideosForAutoCompleteQuery, Envelope<VideosForAutoCompleteResponse>>
    {
        #region Private Fields

        private readonly IVideoUseCase _videoUseCase;

        #endregion Private Fields

        #region Public Constructors

        public GetVideosForAutoCompleteQueryHandler(IVideoUseCase videoUseCase)
        {
            _videoUseCase = videoUseCase;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<Envelope<VideosForAutoCompleteResponse>> Handle(GetVideosForAutoCompleteQuery request, CancellationToken cancellationToken)
        {

            var response = await _videoUseCase.GetVideos(new GetVideosQuery
            {
                PageSize = request.PageSize,
                SearchText = request.SearchText
            });

            if (response?.Payload?.Videos?.Items is null)
            {
                return Envelope<VideosForAutoCompleteResponse>.Result.NotFound();
            }

            var responseForAutoComplete = new VideosForAutoCompleteResponse
            {
                Videos = new PagedList<VideoItemForAutoComplete>()
            };

            foreach (var video in response.Payload.Videos.Items)
            {
                responseForAutoComplete.Videos.Items.Add(new VideoItemForAutoComplete
                {
                    Id = video.Id,
                    Title = video.Title,
                    Duration = video.Duration,
                    ThumbnailUrl = video.ThumbnailUrl,
                });
            }

            return Envelope<VideosForAutoCompleteResponse>.Result.Ok(responseForAutoComplete);
        }

        #endregion Public Methods

    }

    #endregion Public Classes
}
