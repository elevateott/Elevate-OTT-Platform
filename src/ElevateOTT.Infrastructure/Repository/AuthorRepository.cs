using Ardalis.GuardClauses;
using ElevateOTT.Application.Common.Interfaces.Repository;
using ElevateOTT.Application.Features.Content.Authors.Queries.GetAuthors;
using ElevateOTT.Domain.Entities.Content;

namespace ElevateOTT.Infrastructure.Repository
{
    public sealed class AuthorRepository : RepositoryBase<AuthorModel>, IAuthorRepository
    {
        public AuthorRepository(ApplicationDbContext applicationDbContext)
        : base(applicationDbContext)
        {
        }

        public IQueryable<AuthorModel>? GetAuthors(Guid tenantId, GetAuthorsQuery? request, bool trackChanges)
        {
            Guard.Against.Null(request, nameof(request));

            var query = FindAll(trackChanges)
                .Where(a => a.TenantId.Equals(tenantId));

            if (!string.IsNullOrWhiteSpace(request.SearchText))
                query = query.Where(r => r.Name.Contains(request.SearchText));

            query = !string.IsNullOrWhiteSpace(request.SortBy)
                ? query.SortBy(request.SortBy)
                : query.OrderBy(a => a.Name);

            return query;
        }

        public async Task<AuthorModel?> GetAuthorAsync(Guid tenantId, Guid authorId, bool trackChanges) =>
            await FindByCondition(a => a.TenantId.Equals(tenantId)
                                       && a.Id.Equals(authorId), trackChanges)
            .SingleOrDefaultAsync();

        public async Task<AuthorModel?> FindAuthorByConditionAsync(Expression<Func<AuthorModel, bool>> expression, bool trackChanges) =>
            await FindByCondition(expression, trackChanges)
                .SingleOrDefaultAsync();

        public void CreateAuthorForTenant(Guid tenantId, AuthorModel author)
        {
            author.TenantId = tenantId;
            author.CreatedOn = DateTime.Now;
            Create(author);
        }

        public Task<IEnumerable<AuthorModel>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges)
        {
            throw new NotImplementedException();
        }

        public void DeleteAuthor(AuthorModel author) => Delete(author);

        public async Task<bool> AuthorExistsAsync(Expression<Func<AuthorModel, bool>> expression) => await ExistsAsync(expression);
    }
}
