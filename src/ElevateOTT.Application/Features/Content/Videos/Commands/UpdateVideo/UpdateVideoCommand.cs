﻿using System.ComponentModel.DataAnnotations;
using ElevateOTT.Application.Common.Interfaces.UseCases.Content;
using ElevateOTT.Domain.Common.DTOs;

namespace ElevateOTT.Application.Features.Content.Videos.Commands.UpdateVideo;

public class UpdateVideoCommand : BaseAssetDto, IRequest<Envelope<string>>
{

    #region Public Properties

    public Guid Id { get; set; }
    public bool Mp4Support { get; set; }

    public bool HasOneTimePurchasePrice { get; set; }
    public decimal OneTimePurchasePrice { get; set; }
    public bool HasRentalPrice { get; set; }
    public RentalDuration RentalDuration { get; set; }
    public decimal RentalPrice { get; set; }

    public Guid? TrailerVideoId { get; set; }
    public Guid? FeaturedCategoryVideoId { get; set; }

    public IFormFile? PlayerImage { get; set; }
    public IFormFile? CatalogImage { get; set; }
    public IFormFile? FeaturedCatalogImage { get; set; }
    public IFormFile? AnimatedGif { get; set; }

    public ImageState PlayerImageState { get; set; }
    public ImageState CatalogImageState { get; set; }
    public ImageState FeaturedCatalogImageState { get; set; }
    public ImageState AnimatedGifState { get; set; }

    public Guid? AuthorId { get; set; }

    public List<Guid>? CategoryIds { get; set; }

    #endregion Public Properties


    #region Public Classes

    public class UpdateVideoCommandHandler : IRequestHandler<UpdateVideoCommand, Envelope<string>>
    {
        #region Private Fields

        private readonly IVideoUseCase _videoUseCase;

        #endregion Private Fields

        #region Public Constructors

        public UpdateVideoCommandHandler(IVideoUseCase videoUseCase)
        {
            _videoUseCase = videoUseCase;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<Envelope<string>> Handle(UpdateVideoCommand request, CancellationToken cancellationToken)
        {
            return await _videoUseCase.EditVideo(request);
        }

        #endregion Public Methods
    }

    #endregion Public Classes
}
