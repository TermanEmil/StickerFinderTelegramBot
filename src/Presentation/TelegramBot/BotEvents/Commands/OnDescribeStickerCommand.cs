using System;
using System.Threading;
using System.Threading.Tasks;
using Application;
using MediatR;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot.BotEvents.Commands
{
    public class OnDescribeStickerCommand : INotification
    {
        public OnDescribeStickerCommand(Message message, string description)
        {
            Message = message ?? throw new ArgumentNullException(nameof(message));
            Description = description ?? throw new ArgumentNullException(nameof(description));
        }

        public Message Message { get; }
        public string Description { get; }
    }

    public class OnDescribeStickerCommandHandler : INotificationHandler<OnDescribeStickerCommand>
    {
        private readonly IMediator mediator;
        private readonly TelegramBotClient botClient;

        public OnDescribeStickerCommandHandler(IMediator mediator, TelegramBotClient botClient)
        {
            this.mediator = mediator;
            this.botClient = botClient;
        }

        public async Task Handle(OnDescribeStickerCommand notification, CancellationToken ct)
        {
            var message = notification.Message;

            if (message.ReplyToMessage?.Sticker is null)
            {
                await botClient.SendTextMessageAsync(message.Chat, "Must be a reply to a sticker", cancellationToken: ct);
                return;
            }

            var sticker = message.ReplyToMessage.Sticker;
            var fromId = message.From.Id.ToString();

            await mediator.Send(new EnsureStickerIsRegisteredCommand(sticker.FileUniqueId, sticker.FileId), ct);
            await mediator.Send(new EnsureUserIsRegisteredCommand(fromId), ct);
            await mediator.Send(new DescribeStickerCommand(fromId, sticker.FileUniqueId, notification.Description), ct);

            await botClient.SendTextMessageAsync(message.Chat, "Sticker described", cancellationToken: ct);
        }
    }
}
