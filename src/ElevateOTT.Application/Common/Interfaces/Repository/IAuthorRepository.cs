using ElevateOTT.Application.Features.Content.Authors.Queries.GetAuthors;
using ElevateOTT.Domain.Entities.Content;

namespace ElevateOTT.Application.Common.Interfaces.Repository
{
    public interface IAuthorRepository
    {
        Task<AuthorModel?> GetAuthorAsync(Guid tenantId, Guid authorId, bool trackChanges);
        IQueryable<AuthorModel>? GetAuthors(Guid tenantId, GetAuthorsQuery request, bool trackChanges);
        Task<AuthorModel?> FindAuthorByConditionAsync(Expression<Func<AuthorModel, bool>> expression, bool trackChanges);
        void CreateAuthorForTenant(Guid tenantId, AuthorModel author);
        Task<IEnumerable<AuthorModel>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges);
        void DeleteAuthor(AuthorModel author);
        Task<bool> AuthorExistsAsync(Expression<Func<AuthorModel, bool>> expression);
    }
}
