using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevateOTT.Application.Features.Content.Videos.Queries.GetVideosForAutoComplete;

public class VideoItemForAutoComplete
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public TimeSpan? Duration { get; set; }
    public string? ThumbnailUrl { get; set; }
}
