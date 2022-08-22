using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevateOTT.Application.Features.Content.Videos.Queries.GetVideosForAutoComplete;

public class VideosForAutoCompleteResponse
{
    #region Public Properties

    public PagedList<VideoItemForAutoComplete>? Videos { get; set; }

    #endregion Public Properties
}
