using System;
using System.Threading;
using System.Threading.Tasks;
using DataAccess;
using MediatR;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace TelegramBot.BotEvents.OnMessage
{
    public class OnErrorEvent : INotification
    {
        public OnErrorEvent(Chat chat, Exception exception)
        {
            Chat = chat;
            Exception = exception;
        }

        public Chat Chat { get; }
        public Exception Exception { get; }
    }

    public class OnErrorEventHandler : INotificationHandler<OnErrorEvent>
    {
        private readonly ITelegramBotClient botClient;

        public OnErrorEventHandler(ITelegramBotClient botClient)
        {
            this.botClient = botClient;
        }

        public Task Handle(OnErrorEvent notification, CancellationToken ct)
        {
            return botClient.SendTextMessageAsync(
                notification.Chat,
                $"<i>{BuildErrorMessage(notification.Exception)}</i>",
                ParseMode.Html,
                cancellationToken: ct);
        }

        private static string BuildErrorMessage(Exception exception)
        {
            switch (exception)
            {
                case NotFoundException e:
                    return e.Message;

                default:
                    return "Internal error";
            }
        }
    }
}
