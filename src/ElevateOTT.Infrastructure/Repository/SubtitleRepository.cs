﻿using ElevateOTT.Application.Common.Interfaces.Repository;
using ElevateOTT.Domain.Entities.Content;

namespace ElevateOTT.Infrastructure.Repository
{
    public sealed class SubtitleRepository : RepositoryBase<SubtitleModel>, ISubtitleRepository
    {
        public SubtitleRepository(RepositoryContext applicationDbContext)
        : base(applicationDbContext)
        {
        }


        //public Task<PagedList<SubtitleModel>> GetSubtitlesAsync(Guid tenantId, SubtitleParameters subtitleParameters, bool trackChanges)
        //{
        //    throw new NotImplementedException();
        //}

        public Task<SubtitleModel?> GetSubtitleAsync(Guid subtitleId, bool trackChanges)
        {
            throw new NotImplementedException();
        }

        public Task<SubtitleModel?> FindSubtitleByConditionAsync(Expression<Func<SubtitleModel, bool>> expression, bool trackChanges)
        {
            throw new NotImplementedException();
        }

        public void CreateSubtitleForTenant(Guid tenantId, SubtitleModel subtitle)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<SubtitleModel>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges)
        {
            throw new NotImplementedException();
        }

        public void DeleteSubtitle(SubtitleModel subtitle)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SubtitleExistsAsync(Expression<Func<SubtitleModel, bool>> expression) => await ExistsAsync(expression);
    }
}
