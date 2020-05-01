using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DataAccess;
using MediatR;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Utilities.Exceptions;

namespace TelegramBot.BotEvents
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
                case ValidationException e:
                    return BuildValidationErrorMessage(e);

                case NotFoundException _:
                    return "Not Found";

                default:
                    return "Internal error";
            }
        }

        private static string BuildValidationErrorMessage(ValidationException validationException)
        {
            var error = new StringBuilder();
            foreach (var (_, failures) in validationException.Failures)
                error.AppendLine($"{string.Join(",", failures)}");

            return error.ToString();
        }
    }
}
