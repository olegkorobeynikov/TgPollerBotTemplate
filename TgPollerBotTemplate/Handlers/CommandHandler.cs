using NLog;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TgPollerBotTemplate.Handlers
{
    public class CommandHandler
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private static ITelegramBotClient BotClient { get; set; }
        private static CommandHandler _commandHandler { get; set; }

        private CommandHandler(ITelegramBotClient botClient)
        {
            BotClient = botClient ?? throw new Exception($"The parameter [{nameof(botClient)}] can't be null.");
        }

        public static CommandHandler GetInstance(ITelegramBotClient botClient)
        {
            if (_commandHandler == null)
            {
                return new CommandHandler(botClient);
            }
            return _commandHandler;
        }

        private static readonly Dictionary<string, Action<Update>> Handlers = new Dictionary<string, Action<Update>>
            {
                { Commands.Start, StartCommandHandler },
            };


        public static bool HandlerExist(string command)
        {
            return Handlers.ContainsKey(command);
        }

        public static Action<Update> GetHandler(string command)
        {
            return Handlers[command];
        }

        static async void StartCommandHandler(Update e)
        {
            var message = e.Message;
            if (message!.Text!.ToLower() == Commands.Start)
            {
                var welcomeMessage = "Hello World!";
                await BotClient.SendTextMessageAsync(message.Chat, welcomeMessage, parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown);
                return;
            }
            else
            {
                return;
            }
        }
    }
}
