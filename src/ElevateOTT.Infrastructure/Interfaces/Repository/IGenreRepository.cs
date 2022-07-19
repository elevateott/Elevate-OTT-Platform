using ElevateOTT.Domain.Entities.Content;

namespace ElevateOTT.Infrastructure.Interfaces.Repository
{
    public interface IGenreRepository
    {
        //Task<PagedList<GenreModel>> GetGenresAsync(Guid tenantId, GenreParameters genreParameters, bool trackChanges);
        Task<GenreModel?> GetGenreAsync(Guid genreId, bool trackChanges);
        Task<GenreModel?> FindGenreByConditionAsync(Expression<Func<GenreModel, bool>> expression, bool trackChanges);
        //void CreateGenreForTenant(Guid tenantId, GenreModel genre);
        Task<IEnumerable<GenreModel>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges);
        void DeleteGenre(GenreModel genre);
        Task<bool> GenreExistsAsync(Expression<Func<GenreModel, bool>> expression);
    }
}
