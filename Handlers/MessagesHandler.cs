using Telegram.Bot;
using Telegram.Bot.Types;

namespace DistributionGetterBot.Handlers
{
	public static class MessagesHandler
	{
		public static async Task GetHelp(ITelegramBotClient botClient,ChatId chatId)
		{
			await botClient.SendTextMessageAsync(chatId,"hello");
		}
	}
}
