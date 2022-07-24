using Azure.Core;
using ElevateOTT.Application.Common.Interfaces.Repository;
using ElevateOTT.Application.Features.Content.Authors.Queries.GetAuthors;
using ElevateOTT.Domain.Entities.Content;
using ElevateOTT.Infrastructure.Repository.Extensions;
using Microsoft.EntityFrameworkCore;

namespace ElevateOTT.Infrastructure.Repository
{
    public sealed class AuthorRepository : RepositoryBase<AuthorModel>, IAuthorRepository
    {
        public AuthorRepository(ApplicationDbContext applicationDbContext)
        : base(applicationDbContext)
        {
        }

        public async Task<PagedList<AuthorModel>> GetAuthorsAsync(Guid tenantId, GetAuthorsQuery request, bool trackChanges)
        {
            var query = FindAll(trackChanges)
                .Where(a => a.TenantId.Equals(tenantId))
                .Search(request.SearchText)
                .Sort(request.SortBy);

            return await query.ToPagedListAsync(request.PageNumber, request.PageSize);
        }

        public async Task<AuthorModel?> GetAuthorAsync(Guid tenantId, Guid authorId, bool trackChanges) =>
            await FindByCondition(a => a.TenantId.Equals(tenantId)
                                       && a.Id.Equals(authorId), trackChanges)
            .SingleOrDefaultAsync();

        public async Task<AuthorModel?> FindAuthorByConditionAsync(Expression<Func<AuthorModel, bool>> expression, bool trackChanges) =>
            await FindByCondition(expression, trackChanges)
                .SingleOrDefaultAsync();

        public async Task CreateAuthorForTenant(Guid tenantId, AuthorModel author)
        {
            author.TenantId = tenantId;
            author.CreatedOn = DateTime.Now;
            await CreateAsync(author);
        }

        public Task<IEnumerable<AuthorModel>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges)
        {
            throw new NotImplementedException();
        }

        public void DeleteAuthor(AuthorModel author) => Delete(author);

        public async Task<bool> AuthorExistsAsync(Expression<Func<AuthorModel, bool>> expression) => await ExistsAsync(expression);
    }
}
