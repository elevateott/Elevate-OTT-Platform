using ElevateOTT.Domain.Entities.Content;

namespace ElevateOTT.Infrastructure.Interfaces.Repository
{
    public interface IAuthorRepository
    {
        //Task<PagedList<AuthorModel>> GetAuthorsAsync(Guid tenantId, AuthorParameters authorParameters, bool trackChanges);
        Task<AuthorModel?> GetAuthorAsync(Guid authorId, bool trackChanges);
        Task<AuthorModel?> FindAuthorByConditionAsync(Expression<Func<AuthorModel, bool>> expression, bool trackChanges);
        void CreateAuthorForTenant(Guid tenantId, AuthorModel author);
        Task<IEnumerable<AuthorModel>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges);
        void DeleteAuthor(AuthorModel author);
        Task<bool> AuthorExistsAsync(Expression<Func<AuthorModel, bool>> expression);
    }
}
