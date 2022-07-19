using ElevateOTT.Domain.Entities.Products;
using ElevateOTT.Infrastructure.Interfaces.Repository;
using ElevateOTT.Infrastructure.Repository.Extensions;

namespace ElevateOTT.Infrastructure.Repository
{
    public class ProductFamilyRepository : RepositoryBase<ProductFamilyModel>, IProductFamilyRepository
    {
        public ProductFamilyRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        //public async Task<PagedList<ProductFamilyModel>> GetProductFamiliesAsync(Guid? tenantId, ProductFamilyParameters productFamilyParameters, bool trackChanges)
        //{
        //    var productFamilies = await FindAll(trackChanges)
        //        .Where(a => a.TenantId.Equals(tenantId))
        //        .Search(productFamilyParameters.SearchTerm)
        //        .Sort(productFamilyParameters.OrderBy)
        //        .OrderBy(c => c.Name)
        //        .Skip((productFamilyParameters.PageNumber - 1) * productFamilyParameters.PageSize)
        //        .Take(productFamilyParameters.PageSize)
        //        .ToListAsync();

        //    var count = await FindAll(trackChanges).CountAsync();

        //    return new PagedList<ProductFamilyModel>(productFamilies, count, productFamilyParameters.PageNumber, productFamilyParameters.PageSize);
        //}

        public async Task<ProductFamilyModel?> GetProductFamilyAsync(Guid productFamilyId, bool trackChanges) =>
            await FindByCondition(a => a.Id.Equals(productFamilyId), trackChanges)
                .SingleOrDefaultAsync();

        public async Task<ProductFamilyModel?> FindProductFamilyByConditionAsync(Expression<Func<ProductFamilyModel, bool>> expression, bool trackChanges) =>
            await FindByCondition(expression, trackChanges)
                .SingleOrDefaultAsync();

        //public void CreateProductFamily(Guid? tenantId, ProductFamilyModel productFamily)
        //{
        //    productFamily.TenantId = tenantId;
        //    Create(productFamily);
        //}

        public async Task<IEnumerable<ProductFamilyModel>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges) =>
            await FindByCondition(x => ids.Contains(x.Id), trackChanges).ToListAsync();

        public void DeleteProductFamily(ProductFamilyModel productFamily) => Delete(productFamily);

        public async Task<bool> ProductFamilyExistsAsync(Expression<Func<ProductFamilyModel, bool>> expression) => await ExistsAsync(expression);
    }
}
