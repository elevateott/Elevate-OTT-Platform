using ElevateOTT.Application.Features.Content.Videos.Queries.GetSasToken;

namespace ElevateOTT.Application.Common.Interfaces.Services.StorageServices;

public interface IFileStorageService
{
    #region Public Methods
    SasTokenResponse? GetSasTokenForVideoContainer();

    Task<string?> UploadFile(IFormFile formFile, string containerName, string fileNamePrefix);

    Task<List<FileMetaData>> UploadMultipleFiles(IList<IFormFile> formFiles, string containerName, string fileNamePrefix, int defaultFileIndex = 0, string subContainerName = "attachments");

    Task<string?> EditFile(IFormFile formFile, string containerName, string fileNamePrefix, string oldFileUri);

    Task DeleteFileIfExists(string fileUri, string containerName);

    Task DeleteContainer(string containerName, string subContainerName);

    FileStatus GetFileState(IFormFile? formFile, string? oldUrl);

    Task<string> SaveContentFeed(string jsonString, string fileNamePrefix, int? version);

    #endregion Public Methods
}
