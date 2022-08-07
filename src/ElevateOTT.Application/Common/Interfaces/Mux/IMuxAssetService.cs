using ElevateOTT.Application.Features.Content.Videos.Commands.CreateAssetAtMux;
using ElevateOTT.Domain.Entities.Content;

namespace ElevateOTT.Application.Common.Interfaces.Mux;

public interface IMuxAssetService
{
    Task GetAssetFromMux(string assetId);
    Task ListAssetsFromMuxByTenant(Guid tenantId);
    Task<CreateAssetAtMuxResponse> CreateAssetAtMux(CreateAssetAtMuxCommand createAssetAtMuxCommand);
    Task UpdateAssetAtMux();
    Task DeleteAssetFromMux(string assetId);
}
