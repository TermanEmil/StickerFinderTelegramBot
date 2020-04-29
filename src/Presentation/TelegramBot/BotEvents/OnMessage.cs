using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Telegram.Bot.Types;
using TelegramBot.Commands;
using TelegramBot.Commands.OnDescribeSticker;
using TelegramBot.Commands.OnListDescriptions;

namespace TelegramBot.BotEvents
{
    public class OnMessage : INotification
    {
        public OnMessage(Message message)
        {
            Message = message ?? throw new ArgumentNullException(nameof(message));
        }

        public Message Message { get; }
    }

    public class OnMessageHandler : INotificationHandler<OnMessage>
    {
        private readonly IMediator mediator;

        public OnMessageHandler(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task Handle(OnMessage notification, CancellationToken ct)
        {
            var message = notification.Message;
            if (message.Text is null)
                return;

            await TryExecuteCommand(message, "/describe", s => mediator.Publish(new OnDescribeStickerCommand(message, s), ct));
            await TryExecuteCommand(message, "/list", s => mediator.Publish(new OnListDescriptionsCommand(message), ct));
        }

        private static Task TryExecuteCommand(Message message, string command, Func<string, Task> action)
        {
            var messageText = message.Text.ToLower().Trim();
            if (!messageText.StartsWith(command))
                return Task.CompletedTask;

            var finalCommand = messageText.Replace(command, "");
            return action(finalCommand);
        }
    }
}
