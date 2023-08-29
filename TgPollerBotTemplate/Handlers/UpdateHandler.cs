using NLog;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TgPollerBotTemplate.Handlers
{
    internal class UpdateHandler
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            Logger.Info(Newtonsoft.Json.JsonConvert.SerializeObject(update));

            if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message || update.Type == Telegram.Bot.Types.Enums.UpdateType.CallbackQuery)
            {
                var command = "";
                long chatId = 0;
                if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
                {
                    command = Commands.Parse(update.Message!.Text!);;
                    chatId = update.Message.Chat.Id;
                }
                if (update.Type == Telegram.Bot.Types.Enums.UpdateType.CallbackQuery)
                {
                    command = Commands.Parse(update.CallbackQuery!.Data!);
                    chatId = update.CallbackQuery.From.Id;
                }

                if (CommandHandler.HandlerExist(command))
                {
                    try
                    {
                        var handler = CommandHandler.GetHandler(command);
                        handler(update);
                    }
                    catch (Exception ex)
                    {
                        Logger.Error($"Error happened while handle operation. Error: [{ex.ToString}]");
                        await botClient.SendTextMessageAsync(chatId, "Failed to implement command. We're already fixing it. Try again later. Thank you for your patience!", cancellationToken: cancellationToken);
                    }
                }
                else
                {
                    await botClient.SendTextMessageAsync(chatId, "Unknown command. Please use one of the available commands.", cancellationToken: cancellationToken);
                }
            }
        }
    }
}
