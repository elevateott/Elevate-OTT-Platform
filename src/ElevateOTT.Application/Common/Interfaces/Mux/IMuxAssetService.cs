using ElevateOTT.Application.Features.Content.Videos.Commands.CreateAssetAtMux;
using ElevateOTT.Domain.Entities.Content;

namespace ElevateOTT.Application.Common.Interfaces.Mux;

public interface IMuxAssetService
{
    Task GetAssetFromMuxAsync(string assetId);
    Task ListAssetsFromMuxByTenantAsync(Guid tenantId);
    Task<CreateAssetAtMuxResponse> CreateAssetAtMuxAsync(CreateAssetAtMuxCommand createAssetAtMuxCommand);
    Task UpdateAssetAtMuxAsync();
    Task DeleteAssetFromMuxAsync(string assetId);
    Task<bool> AssetExistsAsync(string assetId);

}
