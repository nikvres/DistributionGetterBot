using DistributionGetterBot.Models;
using DistributionGetterBot.Parser;

namespace DistributionGetterBot.Database
{
	public static class DatabaseDAL
	{
		public static async Task AddDistributionToDatabase(string name)
		{
			using var db = new ApplicationContext();
			var distParser = new DistributionInfoParser();
			await db.AddAsync(distParser.ParseInformationAboutDistribution(name).Result);
			db.SaveChanges();
		}
		public static Distribution GetDistributionFromDatabase(string name)
		{
			using var db = new ApplicationContext();
			return db.Distribution.ToList().Where((item) => item.ValueDistribution! == name).First();
		}
		public static List<Distribution> GetAllDistributionsFromDatabase()
		{
			using var db = new ApplicationContext();
			return db.Distribution.ToList();
		}
		public static async Task AddAllDistributions()
		{
			var allDistributionsList = UtilsParser.GetAllAvailableDistributionValues();
			foreach (var distribution in allDistributionsList.Result)
			{
				await AddDistributionToDatabase(distribution);
			}
		}
	}
}
