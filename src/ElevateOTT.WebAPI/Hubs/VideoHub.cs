namespace ElevateOTT.WebAPI.Hubs;

//[Authorize]
public class VideoHub : Hub
{
    //ref: https://newbedev.com/call-signalr-core-hub-method-from-controller

    #region Private Fields

    private readonly IBackgroundJobClient _backgroundJob;

    private readonly ISignalRContextProvider _signalRContextProvider;

    #endregion Private Fields

    public static string ReceiveUpdateMethod => "ReceiveUpdate";

    public override async Task OnConnectedAsync()
    {
        Console.WriteLine("Connected!");

        if (Context.User?.Identity != null)
        {
            var name = _signalRContextProvider.GetUserName(Context);
            await Groups.AddToGroupAsync(Context.ConnectionId, name);
        }

        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        Console.WriteLine("Disconnected!");
    }

    //public Task NotifyVideoCreationStatus(Guid? videoId, AssetCreationStatus status)
    //{
    //    return Clients.Caller.SendAsync(ReceiveUpdateMethod, videoId, status);
    //}
}
