using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElevateOTT.Application.Common.Interfaces.Persistence;

namespace ElevateOTT.Application.Common.Interfaces.Services;

public interface IContentFeedService
{
    Task CreateContentFeed(IApplicationDbContext dbContext);
}
