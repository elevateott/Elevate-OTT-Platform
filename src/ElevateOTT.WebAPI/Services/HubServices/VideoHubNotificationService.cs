namespace ElevateOTT.WebAPI.Services.HubServices;

public class VideoHubNotificationService: IVideoHubNotificationService
{
    #region Private Fields

    private readonly IHubContext<VideoHub> _hubContext;

    public VideoHubNotificationService(IHubContext<VideoHub> hubContext)
    {
        _hubContext = hubContext;
    }

    #endregion Private Fields

    public async Task NotifyCreationStatus(string userNameIdentifier, Guid videoId, AssetCreationStatus status)
    {
        await _hubContext.Clients.User(userNameIdentifier).SendAsync("NotifyCreationStatus", videoId, status);
    }
}
