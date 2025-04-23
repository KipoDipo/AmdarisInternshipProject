using HiFive.Application.Contracts.Repositories;
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

	public async Task AddAsync(T entity)
	{
		entity.CreatedOn = DateTime.Now;
		entity.UpdatedOn = DateTime.Now;
		await _dbContext.AddAsync(entity);
		await _dbContext.SaveChangesAsync();
	}

	public IQueryable<T> GetAll()
	{
		return _dbContext.Set<T>().AsQueryable();
	}

	public IQueryable<T> GetAllNoTracking()
	{
		return _dbContext.Set<T>().AsNoTracking().AsQueryable();
	}

	// Will use Tracker
	public async Task<T?> GetByIdAsync(Guid id)
	{
		return await _dbContext.Set<T>().FindAsync(id);
	}

	public abstract Task<T?> GetWithDetailsByIdAsync(Guid id);

	public async Task UpdateAsync(T entity)
	{
		entity.UpdatedOn = DateTime.Now;
		_dbContext.Update(entity);
		await _dbContext.SaveChangesAsync();
	}

	public async Task DeleteAsync(Guid id)
	{
		var entity = await _dbContext.Set<T>().FindAsync(id);
		if (entity == null)
			throw new InvalidOperationException($"Entity with id {id} not found.");

		entity.IsDeleted = true;
		entity.DeletedOn = DateTime.Now;
		await _dbContext.SaveChangesAsync();
	}

}
