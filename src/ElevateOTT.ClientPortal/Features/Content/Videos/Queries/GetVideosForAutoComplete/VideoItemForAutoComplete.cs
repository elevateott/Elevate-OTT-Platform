namespace ElevateOTT.ClientPortal.Features.Content.Videos.Queries.GetVideosForAutoComplete;

public class VideoItemForAutoComplete
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public TimeSpan? Duration { get; set; }
    public string? ThumbnailUrl { get; set; }
}
