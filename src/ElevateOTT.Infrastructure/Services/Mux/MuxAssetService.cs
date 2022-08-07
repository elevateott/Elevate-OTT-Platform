using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElevateOTT.Application.Common.Interfaces.Mux;
using ElevateOTT.Application.Common.Interfaces.Repository;
using ElevateOTT.Application.Common.Models.ApplicationOptions;
using ElevateOTT.Application.Features.Content.Videos.Commands.CreateAssetAtMux;
using ElevateOTT.Domain.Entities.Content;
using Mux.Csharp.Sdk.Api;
using Mux.Csharp.Sdk.Client;
using Mux.Csharp.Sdk.Model;

namespace ElevateOTT.Infrastructure.Services.Mux;

public class MuxAssetService : IMuxAssetService
{
    private readonly IConfigReaderService _configReaderService;
    private readonly ILogger<MuxAssetService> _logger;
    private readonly Configuration _muxConfig;
    private MuxOptions _muxOptions;

    public MuxAssetService(ILogger<MuxAssetService> logger,
        IRepositoryManager repository, IConfigReaderService configReaderService)
    {
        _logger = logger;
        _configReaderService = configReaderService;
        _muxOptions = _configReaderService.GetMuxOptions();

        _muxConfig = new Configuration
        {
            BasePath = _muxOptions.BasePath,
            Username = _muxOptions.Username,
            Password = _muxOptions.Password
        };
    }

    //    list assets
    //    get asset
    //    delete asset
    //    update asset
    //    get asset input info
    //    create playback id
    //    get playback id
    //    delete playback id
    //    update mp4 support
    //    update master access
    //    create asset track
    //    delete asset track

    public Task GetAssetFromMux(string assetId)
    {
        throw new NotImplementedException();
    }

    public Task ListAssetsFromMuxByTenant(Guid tenantId)
    {
        throw new NotImplementedException();
    }

    public async Task<CreateAssetAtMuxResponse> CreateAssetAtMux(CreateAssetAtMuxCommand createAssetAtMuxCommand)
    {
        var apiInstance = new AssetsApi(_muxConfig);

        var input = new InputSettings(createAssetAtMuxCommand.BlobUrl, 
            languageCode: createAssetAtMuxCommand.LanguageCode, 
            closedCaptions: createAssetAtMuxCommand.ClosedCaption);

        // ref: https://docs.mux.com/api-reference/video#operation/create-asset

        var assetCreationRequest = new CreateAssetRequest
        {
            Input = new List<InputSettings> { input },
            PlaybackPolicy = new List<PlaybackPolicy> { PlaybackPolicy.Public },
            Test = createAssetAtMuxCommand.IsTestAsset,
            Mp4Support = createAssetAtMuxCommand.Mp4Support ? CreateAssetRequest.Mp4SupportEnum.Standard : CreateAssetRequest.Mp4SupportEnum.None,
            Passthrough = createAssetAtMuxCommand.Passthrough
        };

        var asset = await apiInstance.CreateAssetAsync(assetCreationRequest);
        return new CreateAssetAtMuxResponse { AssetId = asset.Data.Id };
    }

    public Task UpdateAssetAtMux()
    {
        throw new NotImplementedException();
    }

    public Task DeleteAssetFromMux(string assetId)
    {
        throw new NotImplementedException();
    }
}
