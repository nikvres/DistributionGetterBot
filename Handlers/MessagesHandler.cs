using DistributionGetterBot.Database;
using DistributionGetterBot.Models;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace DistributionGetterBot.Handlers
{
	public static class MessagesHandler
	{
        public static async Task GetStarted(ITelegramBotClient botClient, ChatId chatId, User user)
        {
            await botClient.SendTextMessageAsync(chatId, "Hello, this is distribution bot.");
			await DatabaseDAL.AddUserToDatabase(user);
        }
        public static async Task GetHelp(ITelegramBotClient botClient, ChatId chatId)
		{
			await botClient.SendTextMessageAsync(chatId, "hello");
		}
		public static async Task GetDistribution(ITelegramBotClient botClient, ChatId chatId, string messageText)
		{
			try
			{
				var splittedMessage = messageText.Split();
				if (splittedMessage.Length.Equals(2))
                {
                    Distribution distribution = DatabaseDAL.GetDistributionFromDatabase(splittedMessage[1]);
                    await using Stream stream = System.IO.File.OpenRead(distribution.PictureDistribution!);
                    await botClient.SendPhotoAsync(chatId, stream);
                    await botClient.SendTextMessageAsync(chatId, distribution.GetFields());
                }
				else
				{
					await botClient.SendTextMessageAsync(chatId, "Usage: /dist <distribution_name>");
				}
                
			}
			catch (InvalidOperationException)
			{
				await botClient.SendTextMessageAsync(chatId, "There is no distribution in database");
			}

		}
	}
}
