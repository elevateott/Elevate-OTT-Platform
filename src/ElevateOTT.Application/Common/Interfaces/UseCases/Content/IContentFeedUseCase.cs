using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElevateOTT.Application.Features.Content.ContentFeeds.Queries.GetContentFeed;

namespace ElevateOTT.Application.Common.Interfaces.UseCases.Content;

public interface IContentFeedUseCase
{
    Task<Envelope<ContentFeedItem>> GetContentFeedUrl(GetContentFeedUrlQuery request);

}
