using NLog;
using Telegram.Bot;

namespace TgPollerBotTemplate.Handlers
{
    internal class ErrorHandler
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public static async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            Logger.Error(Newtonsoft.Json.JsonConvert.SerializeObject(exception));
        }
    }
}
