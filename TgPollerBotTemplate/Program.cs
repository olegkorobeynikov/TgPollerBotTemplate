using NLog;
using Telegram.Bot.Polling;
using Telegram.Bot;
using TgPollerBotTemplate.Handlers;

namespace TgPollerBotTemplate
{
    internal class Program
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private static readonly string TokenFile = "token";

        static ITelegramBotClient bot = new TelegramBotClient(ReadToken(TokenFile));

        static void Main(string[] args)
        {
            Logger.Info($"Bot started [{bot.GetMeAsync().Result.FirstName}]");

            CommandHandler.GetInstance(bot);

            var cts = new CancellationTokenSource();
            var cancellationToken = cts.Token;
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = { }, // receive all update types
            };
            bot.StartReceiving(
                UpdateHandler.HandleUpdateAsync,
                ErrorHandler.HandleErrorAsync,
                receiverOptions,
                cancellationToken
            );
            Console.ReadLine();
        }

        public static string ReadToken(string filePath)
        {
            return File.ReadAllText(filePath);
        }
    }
}