using ElevateOTT.Application.Common.Interfaces.Repository;

namespace ElevateOTT.Infrastructure.Repository
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
	{
		protected ApplicationDbContext ApplicationDbContext;

		public RepositoryBase(ApplicationDbContext applicationDbContext)
			=> ApplicationDbContext = applicationDbContext;

		public IQueryable<T> FindAll(bool trackChanges) =>
			!trackChanges ?
			  ApplicationDbContext.Set<T>()
				.AsNoTracking() :
			  ApplicationDbContext.Set<T>();

		public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression,
		bool trackChanges) =>
			!trackChanges ?
			  ApplicationDbContext.Set<T>()
				.Where(expression)
				.AsNoTracking() :
			  ApplicationDbContext.Set<T>()
				.Where(expression);

		public async Task CreateAsync(T entity) => await ApplicationDbContext.Set<T>().AddAsync(entity);

		public void Update(T entity) => ApplicationDbContext.Set<T>().Update(entity);

		public void Delete(T entity) => ApplicationDbContext.Set<T>().Remove(entity);

        public async Task<bool> ExistsAsync(Expression<Func<T, bool>> expression) 
            => await ApplicationDbContext.Set<T>().AnyAsync(expression);
    }
}
