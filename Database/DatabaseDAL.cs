using DistributionGetterBot.Models;
using DistributionGetterBot.Parser;
using Telegram.Bot.Types;

namespace DistributionGetterBot.Database
{
	public static class DatabaseDAL
	{
		public static async Task AddDistributionToDatabase(string name)
		{
			using var db = new ApplicationContext();
			var distParser = new DistributionParser();
			await db.AddAsync(distParser.ParseInformationAboutDistribution(name).Result);
			db.SaveChanges();
		}
		public static Distribution GetDistributionFromDatabase(string name)
		{
			using var db = new ApplicationContext();
			return db.Distribution.ToList().Where((item) => item.ValueDistribution! == name).First();
		}
		public static List<Distribution> GetAllDistributionSsFromDatabase()
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
		public static async Task AddUserToDatabase(User user)
		{
            using var db = new ApplicationContext();
			await db.User.AddAsync(new UserModel(Convert.ToString(user.Id), user.Username, user.FirstName, user.LastName));
        }
    }
}
