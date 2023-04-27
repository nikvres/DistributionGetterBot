using DistributionGetterBot.Models;
using DistributionGetterBot.Parser;
using Microsoft.EntityFrameworkCore;

namespace DistributionGetterBot.Database
{
	public static class DatabaseAdapter
	{
		public static async Task AddDistributionToDatabase(string name)
		{
			using var db = new ApplicationContext();
			var distParser = new DistributionInfoParser();
			await db.AddAsync(distParser.ParseInformationAboutDistribution(name));
			db.SaveChanges();
		}
		public static Distribution GetDistributionFromDatabase(string name)
		{
			using var db = new ApplicationContext();
			return db.Distribution.ToList().Where((item) => item.name_distribution!.Contains(name)).First();	
		}
	}
}
