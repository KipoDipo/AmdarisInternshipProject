using HiFive.Application.Contracts.Repositories;
using HiFive.Application.Exceptions;
using HiFive.Domain.Contracts;
using Microsoft.EntityFrameworkCore;

namespace HiFive.Infrastructure.Repositories;

public abstract class BaseRepository<T> : IRepository<T> where T : class, IDeletable
{
	protected readonly DbContext _dbContext;

	public BaseRepository(DbContext dbContext)
	{
		_dbContext = dbContext;
	}

	public virtual async Task AddAsync(T entity)
	{
		await _dbContext.AddAsync(entity);
		await _dbContext.SaveChangesAsync();
	}

	public virtual IQueryable<T> GetAll()
	{
		return _dbContext.Set<T>().AsQueryable();
	}

	public virtual IQueryable<T> GetAllNoTracking()
	{
		return _dbContext.Set<T>().AsNoTracking().AsQueryable();
	}

	// Will use Tracker
	public virtual async Task<T?> GetByIdAsync(Guid id)
	{
		return await _dbContext.Set<T>().FindAsync(id);
	}

	public virtual async Task UpdateAsync(T entity)
	{
		_dbContext.Update(entity);
		await _dbContext.SaveChangesAsync();
	}

	public virtual async Task DeleteAsync(Guid id)
	{
		var entity = await _dbContext.Set<T>().FindAsync(id);
		if (entity == null)
			throw new NotFoundException($"Entity with id {id} not found.");

		_dbContext.Remove(entity);
		await _dbContext.SaveChangesAsync();
	}

}
