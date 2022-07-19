﻿namespace ElevateOTT.Infrastructure.Interfaces.Repository
{
    public interface IRepositoryBase<T>
    {
		IQueryable<T> FindAll(bool trackChanges);
		IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges);
		void Create(T entity);
		void Update(T entity);
		void Delete(T entity);
        Task<bool> ExistsAsync(Expression<Func<T, bool>> expression);
    }
}