using HiFive.Domain.Contracts;

namespace HiFive.Application.Contracts;

public interface IRepository<T> where T : IBase
{
	Task<T?> GetByIdAsync(Guid id);
	IQueryable<T> Query();
	IQueryable<T> GetAll();
	IQueryable<T> GetAllAsNoTracking();
	Task AddAsync(T entity);
	Task UpdateAsync(T entity);
	Task DeleteAsync(Guid id);
}
