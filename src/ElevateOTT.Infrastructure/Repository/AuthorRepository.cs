using ElevateOTT.Domain.Entities.Content;
using ElevateOTT.Infrastructure.Interfaces.Repository;
using ElevateOTT.Infrastructure.Repository.Extensions;

namespace ElevateOTT.Infrastructure.Repository
{
    public sealed class AuthorRepository : RepositoryBase<AuthorModel>, IAuthorRepository
    {
        public AuthorRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
        {
        }

        //public async Task<PagedList<AuthorModel>> GetAuthorsAsync(Guid tenantId, AuthorParameters authorParameters, bool trackChanges)
        //{
        //    var authors = await FindAll(trackChanges)
        //        .Where(a => a.TenantId.Equals(tenantId))
        //        .Search(authorParameters.SearchTerm ?? string.Empty)
        //        .Sort(authorParameters.OrderBy ?? string.Empty)
        //        .OrderBy(c => c.Name)
        //        .Skip((authorParameters.PageNumber - 1) * authorParameters.PageSize)
        //        .Take(authorParameters.PageSize)
        //        .ToListAsync();

        //    var count = await FindAll(trackChanges).CountAsync();

        //    return new PagedList<AuthorModel>(authors, count, authorParameters.PageNumber, authorParameters.PageSize);
        //}

        public async Task<AuthorModel?> GetAuthorAsync(Guid authorId, bool trackChanges) =>
            await FindByCondition(a => a.Id.Equals(authorId), trackChanges)
            .SingleOrDefaultAsync();

        public async Task<AuthorModel?> FindAuthorByConditionAsync(Expression<Func<AuthorModel, bool>> expression, bool trackChanges) =>
            await FindByCondition(expression, trackChanges)
                .SingleOrDefaultAsync();

        public void CreateAuthorForTenant(Guid tenantId, AuthorModel author)
        {
            throw new NotImplementedException();
        }

        //public void CreateAuthorForTenant(Guid tenantId, AuthorModel author)
        //{
        //    author.TenantId = tenantId;
        //    Create(author);
        //}
        public async Task<IEnumerable<AuthorModel>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges) =>
            await FindByCondition(x => ids.Contains(x.Id), trackChanges).ToListAsync();

        public void DeleteAuthor(AuthorModel author) => Delete(author);
        public async Task<bool> AuthorExistsAsync(Expression<Func<AuthorModel, bool>> expression) => await ExistsAsync(expression);
    }
}
