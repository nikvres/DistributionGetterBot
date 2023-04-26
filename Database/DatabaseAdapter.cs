using DistributionGetterBot.Database;
using DistributionGetterBot.Models;

namespace DistributionGetterBot.Adapters
{
	public static class DatabaseAdapter
	{
		public static async Task AddDistributionToDatabase(string distributionName, Dictionary<string, string> distributionInformation)
		{
			using var db = new ApplicationContext();
			Distribution distribution = new Distribution
			{
				name_distribution = distributionName,
				based_on_distribution = distributionInformation["Based on"],
				os_name = distributionInformation["OS Type"],
				value_distribution = distributionInformation["Value"],
				origin_distribution = distributionInformation["Origin"],
				architecture_distribution = distributionInformation["Architecture"],
				desktop_distribution = distributionInformation["Desktop"],
				category_distribution = distributionInformation["Category"],
				status_distribution = distributionInformation["Status"],
				popularity_distribution = distributionInformation["Popularity"],
				description_distribution = distributionInformation["Description"],
			};
			await db.AddAsync(distribution);
			db.SaveChanges();
		}
	}
}
