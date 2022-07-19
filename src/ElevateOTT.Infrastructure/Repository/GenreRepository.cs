using ElevateOTT.Domain.Entities.Content;
using ElevateOTT.Infrastructure.Interfaces.Repository;
using ElevateOTT.Infrastructure.Repository.Extensions;

namespace ElevateOTT.Infrastructure.Repository
{
    public sealed class GenreRepository : RepositoryBase<GenreModel>, IGenreRepository
    {
        public GenreRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
        {
        }

        //public async Task<PagedList<GenreModel>> GetGenresAsync(Guid tenantId, GenreParameters genreParameters, bool trackChanges)
        //{
        //    var genres = await FindAll(trackChanges)
        //        .Where(a => a.TenantId.Equals(tenantId))
        //        .Search(genreParameters.SearchTerm)
        //        .Sort(genreParameters.OrderBy)
        //        .OrderBy(c => c.Name)
        //        .Skip((genreParameters.PageNumber - 1) * genreParameters.PageSize)
        //        .Take(genreParameters.PageSize)
        //        .ToListAsync();

        //    var count = await FindAll(trackChanges).CountAsync();

        //    return new PagedList<GenreModel>(genres, count, genreParameters.PageNumber, genreParameters.PageSize);
        //}

        public async Task<GenreModel?> GetGenreAsync(Guid genreId, bool trackChanges) =>
            await FindByCondition(a => a.Id.Equals(genreId), trackChanges)
            .SingleOrDefaultAsync();

        public async Task<GenreModel?> FindGenreByConditionAsync(Expression<Func<GenreModel, bool>> expression, bool trackChanges) =>
            await FindByCondition(expression, trackChanges)
                .SingleOrDefaultAsync();

        //public void CreateGenreForTenant(Guid tenantId, GenreModel genre)
        //{
        //    genre.TenantId = tenantId;
        //    Create(genre);
        //}
        public async Task<IEnumerable<GenreModel>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges) =>
            await FindByCondition(x => ids.Contains(x.Id), trackChanges).ToListAsync();

        public void DeleteGenre(GenreModel genre) => Delete(genre);
        public async Task<bool> GenreExistsAsync(Expression<Func<GenreModel, bool>> expression) => await ExistsAsync(expression);
    }
}
