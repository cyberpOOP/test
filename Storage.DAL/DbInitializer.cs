using Microsoft.EntityFrameworkCore;

namespace Storage.DAL
{
	public class DbInitializer : IDbInitializer
	{

		private readonly ApplicationDbContext _context;
		public DbInitializer(ApplicationDbContext context)
		{
			_context = context;
		}
		public void Seed()
		{
			try
			{
				if (_context.Database.GetPendingMigrationsAsync().GetAwaiter().GetResult().Count() > 0)
				{
					_context.Database.Migrate();
				}
			}
			catch (Exception)
			{
				throw;
			}
		}
	}
}
