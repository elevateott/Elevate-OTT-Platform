namespace ElevateOTT.Application.Common.Interfaces.Services.StorageServices
{
    public interface IBlobStorageService
    {
        AzureStorageSASResult? GetSasTokenForVideoContainer(Guid tenantId);
        Task<string> UploadVideoAsync(Stream fileStream, string fileName, string contentType);
        Task<string> UploadImageAsync(Stream fileStream, string fileName, string contentType);
        Task<string> UploadContentFeedAsync(Stream fileStream, string fileName, string contentType);
        Task<bool> DeleteVideoAsync(string fileName);
        Task<bool> DeleteImageAsync(string fileName);
        Task<bool> DeleteContentFeedAsync(string fileName);
    }
}
