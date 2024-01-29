using System.Linq.Expressions;

namespace Storage.DAL.Repositories.Interfaces
{
	public interface IRepository<T> where T : class
	{
		Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>>? filter = null, string? includeProperties = null);

		Task<T> GetFirstOrDefault(Expression<Func<T, bool>> filter, string? includeProperties = null, bool tracked = false);
		Task Add(T entity);
		Task AddRange(IEnumerable<T> entities);
		void Update(T entity);
		void Remove(T entity);
		void RemoveRange(IEnumerable<T> entities);
	}
}
