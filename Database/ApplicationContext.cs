using DistributionGetterBot.Models;
using Microsoft.EntityFrameworkCore;

namespace DistributionGetterBot.Database
{
	public class ApplicationContext : DbContext
	{
		public DbSet<Distribution> Distribution => Set<Distribution>();
		public ApplicationContext() => Database.EnsureCreated();

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer(Environment.GetEnvironmentVariable("connectionString"));
		}
	}
}
