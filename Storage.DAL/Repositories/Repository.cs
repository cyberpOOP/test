using Microsoft.EntityFrameworkCore;
using Storage.DAL.Repositories.Interfaces;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Storage.DAL.Repositories
{
	public class Repository<T> : IRepository<T> where T : class
	{
		private readonly ApplicationDbContext _context;
		internal DbSet<T> dbSet;

		public Repository(ApplicationDbContext context)
		{
			_context = context;
			dbSet = _context.Set<T>();
		}

		public async Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>>? filter = null, string? includeProperties = null)
		{
			IQueryable<T> query = dbSet;
			if (filter != null) query = query.Where(filter);

			if (!string.IsNullOrEmpty(includeProperties))
			{
				foreach (var includeProp in includeProperties
					.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
				{
					query = query.Include(includeProp);
				}
			}
			return await query.ToListAsync();
		}

		public async Task<T> GetFirstOrDefault(Expression<Func<T, bool>> filter, string? includeProperties = null, bool tracked = false)
		{
			IQueryable<T> query = tracked ? dbSet : dbSet.AsNoTracking();

			query = query.Where(filter);
			if (!string.IsNullOrEmpty(includeProperties))
			{
				foreach (var includeProp in includeProperties
					.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
				{
					query = query.Include(includeProp);
				}
			}
			return await query.FirstOrDefaultAsync();
		}
		public async Task Add(T entity)
		{
			await dbSet.AddAsync(entity);
		}
		public async Task AddRange(IEnumerable<T> entities)
		{
			await dbSet.AddRangeAsync(entities);
		}

		public void Update(T entity)
		{
			dbSet.Update(entity);
		}

		public void Remove(T entity)
		{
			dbSet.Remove(entity);
		}

		public void RemoveRange(IEnumerable<T> entities)
		{
			dbSet.RemoveRange(entities);
		}
	}
}
