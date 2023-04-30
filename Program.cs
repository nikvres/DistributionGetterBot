using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

TelegramBotClient client = new TelegramBotClient(Environment.GetEnvironmentVariable("TOKEN")!);

client.StartReceiving(UpdateHandler, ErrorHandler);
Console.ReadKey();

async Task ErrorHandler(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
{
	var ErrorMessage = exception switch
	{
		ApiRequestException apiRequestException => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
		_ => exception.ToString()
	};
	await Task.Run(() => Console.WriteLine(ErrorMessage));
}

async Task UpdateHandler(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
{
	try
	{
		await (update.Type switch
		{
			UpdateType.Message => Task.CompletedTask,
			UpdateType.InlineQuery => Task.CompletedTask,
			UpdateType.CallbackQuery => Task.CompletedTask,
			_ => Task.CompletedTask
		});
	}
	catch (Exception ex)
	{
		Console.WriteLine($"Exception while handling: {update.Type}: {ex}");
	}

}