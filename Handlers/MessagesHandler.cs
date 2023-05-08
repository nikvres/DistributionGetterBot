using DistributionGetterBot.Database;
using DistributionGetterBot.Models;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace DistributionGetterBot.Handlers
{
	public static class MessagesHandler
	{
		public static async Task GetHelp(ITelegramBotClient botClient, ChatId chatId)
		{
			await botClient.SendTextMessageAsync(chatId, "hello");
		}
		public static async Task GetDistribution(ITelegramBotClient botClient, ChatId chatId, string distributionName)
		{
			try
			{
				Distribution distribution = DatabaseDAL.GetDistributionFromDatabase(distributionName);
				await using Stream stream = System.IO.File.OpenRead(distribution.PictureDistribution);
				await botClient.SendPhotoAsync(chatId, stream);
				await botClient.SendTextMessageAsync(chatId, distribution.GetFields());
			}
			catch (InvalidOperationException)
			{
				await botClient.SendTextMessageAsync(chatId, "There is no distribution in database");
			}

		}
	}
}
