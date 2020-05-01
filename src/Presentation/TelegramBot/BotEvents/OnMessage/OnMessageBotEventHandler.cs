using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Telegram.Bot.Types;
using TelegramBot.Commands.OnDescribeSticker;
using TelegramBot.Commands.OnListDescriptions;

namespace TelegramBot.BotEvents.OnMessage
{
    public class OnMessageBotEventHandler : INotificationHandler<OnMessageBotEvent>
    {
        private readonly IMediator mediator;

        public OnMessageBotEventHandler(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task Handle(OnMessageBotEvent notification, CancellationToken ct)
        {
            var message = notification.Message;
            if (message.Text is null)
                return;

            try
            {
                await InnerHandle(message, ct);
            }
            catch (Exception e)
            {
                await mediator.Publish(new OnErrorEvent(message.Chat, e), ct);
            }
        }

        private async Task InnerHandle(Message message, CancellationToken ct)
        {
            await TryExecuteCommand(
                message,
                "/describe",
                s => mediator.Publish(new OnDescribeStickerCommand(message, s), ct));

            await TryExecuteCommand(
                message,
                "/list",
                s => mediator.Publish(new OnListDescriptionsCommand(message), ct));
        }

        private static Task TryExecuteCommand(Message message, string command, Func<string, Task> action)
        {
            var messageText = message.Text.ToLower().Trim();
            if (!messageText.StartsWith(command))
                return Task.CompletedTask;

            var finalCommand = messageText.Replace(command, "").Trim();
            return action(finalCommand);
        }
    }
}