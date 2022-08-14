using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElevateOTT.Application.Common.Interfaces.Repository;
using ElevateOTT.Domain.Entities.Content;

namespace ElevateOTT.Infrastructure.Repository;

public sealed class AssetImageRepository : RepositoryBase<AssetImageModel>, IAssetImageRepository
{
    public AssetImageRepository(ApplicationDbContext applicationDbContext)
        : base(applicationDbContext)
    {
    }
}
