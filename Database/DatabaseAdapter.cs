﻿using DistributionGetterBot.Parser;

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
	}
}
