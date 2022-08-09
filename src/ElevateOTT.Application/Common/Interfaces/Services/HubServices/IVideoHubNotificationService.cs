using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevateOTT.Application.Common.Interfaces.Services.HubServices;

public interface IVideoHubNotificationService
{
    Task NotifyCreationStatus(Guid videoId, AssetCreationStatus status);
}
