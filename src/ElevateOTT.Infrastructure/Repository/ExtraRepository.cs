using ElevateOTT.Domain.Entities.Content;
using ElevateOTT.Infrastructure.Interfaces.Repository;

namespace ElevateOTT.Infrastructure.Repository
{
    public sealed class ExtraRepository : RepositoryBase<ExtraModel>, IExtraRepository
    {
        public ExtraRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
        {
        }


        //public Task<PagedList<ExtraModel>> GetExtrasAsync(Guid tenantId, ExtraParameters extraParameters, bool trackChanges)
        //{
        //    throw new NotImplementedException();
        //}

        public Task<ExtraModel?> GetExtraAsync(Guid extraId, bool trackChanges)
        {
            throw new NotImplementedException();
        }

        public Task<ExtraModel?> FindExtraByConditionAsync(Expression<Func<ExtraModel, bool>> expression, bool trackChanges)
        {
            throw new NotImplementedException();
        }

        public void CreateExtraForTenant(Guid tenantId, ExtraModel extra)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ExtraModel>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges)
        {
            throw new NotImplementedException();
        }

        public void DeleteExtra(ExtraModel extra)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ExtraExistsAsync(Expression<Func<ExtraModel, bool>> expression) => await ExistsAsync(expression);
    }
}
