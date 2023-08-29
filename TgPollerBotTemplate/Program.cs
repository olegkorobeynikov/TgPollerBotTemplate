﻿using NLog;
using Telegram.Bot;
using Telegram.Bot.Polling;
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
            if (!File.Exists(filePath))
            {
                Logger.Error($"The token missing. Add the file [token] to the [bin] directory.");
            }
            return File.ReadAllText(filePath);
        }
    }
}