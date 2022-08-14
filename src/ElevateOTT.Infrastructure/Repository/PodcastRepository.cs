using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElevateOTT.Application.Common.Interfaces.Repository;
using ElevateOTT.Domain.Entities.Content;

namespace ElevateOTT.Infrastructure.Repository;

public sealed class PodcastRepository : RepositoryBase<PodcastModel>, IPodcastRepository
{
    public PodcastRepository(ApplicationDbContext applicationDbContext)
        : base(applicationDbContext)
    {
    }
}
