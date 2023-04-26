namespace DistributionGetterBot.Database
{
	public static class DatabaseAdapter
	{
		public static async Task AddDistributionToDatabase(string distributionName, Dictionary<string, string> distributionInformation)
		{
			using var db = new ApplicationContext();
			await db.AddAsync(db);
			db.SaveChanges();
		}
	}
}
