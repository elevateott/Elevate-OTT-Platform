using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Bibliography;
using ElevateOTT.Application.Common.Interfaces.Repository;
using ElevateOTT.Application.Common.Interfaces.Services;
using ElevateOTT.Application.Common.Interfaces.UseCases.Content;
using ElevateOTT.Application.Features.Content.Categories.Queries.GetCategories;
using ElevateOTT.Application.Features.Content.ContentFeeds.Queries.GetContentFeed;
using ElevateOTT.Application.Features.Content.Videos.Queries.GetVideos;
using ElevateOTT.Domain.Entities;
using ElevateOTT.Domain.Entities.Content;
using Microsoft.EntityFrameworkCore;

namespace ElevateOTT.Application.UseCases.Content;

public class ContentFeedUseCase : IContentFeedUseCase
{
    public Task<Envelope<ContentFeedItem>> GetContentFeedUrl(GetContentFeedUrlQuery request)
    {
        throw new NotImplementedException();
    }
}
