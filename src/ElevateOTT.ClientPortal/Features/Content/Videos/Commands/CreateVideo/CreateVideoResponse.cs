﻿namespace ElevateOTT.ClientPortal.Features.Content.Videos.Commands.CreateVideo;

public class CreateVideoResponse
{
    #region Public Properties

    public Guid Id { get; set; }

    public string SuccessMessage { get; set; } = string.Empty;

    public string BlobUrl { get; set; } = string.Empty;

    public string DownloadUrl { get; set; } = string.Empty;

    public string Passthrough { get; set; } = string.Empty;

    public string LanguageCode { get; set; } = string.Empty;

    public bool ClosedCaptions { get; set; }

    public bool Mp4Support { get; set; }

    public bool IsTestAsset { get; set; }

    #endregion Public Properties
}
