using ElevateOTT.Domain.Entities.Content;

namespace ElevateOTT.Infrastructure.Interfaces.Repository
{
    public interface IExtraRepository
    {
        //Task<PagedList<ExtraModel>> GetExtrasAsync(Guid tenantId, ExtraParameters extraParameters, bool trackChanges);
        Task<ExtraModel?> GetExtraAsync(Guid extraId, bool trackChanges);
        Task<ExtraModel?> FindExtraByConditionAsync(Expression<Func<ExtraModel, bool>> expression, bool trackChanges);
        void CreateExtraForTenant(Guid tenantId, ExtraModel extra);
        Task<IEnumerable<ExtraModel>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges);
        void DeleteExtra(ExtraModel extra);
        Task<bool> ExtraExistsAsync(Expression<Func<ExtraModel, bool>> expression);
    }
}
