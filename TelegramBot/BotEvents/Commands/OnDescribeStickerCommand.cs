using System;
using System.Threading;
using System.Threading.Tasks;
using Application;
using MediatR;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBot.Utilities;

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

            if (message.ReplyToMessage is null || message.ReplyToMessage.Sticker is null)
            {
                await botClient.SendTextMessageAsync(message.Chat, "Must be a reply to a sticker");
                return;
            }

            var stickerId = message.ReplyToMessage.Sticker.FileId;

            await mediator.Send(new EnsureStickerExistsCommand(stickerId), ct);
            await mediator.Send(new EnsureUserExistsCommand(message.From.Id, message.From.FullName()), ct);
            await mediator.Send(new DescribeStickerCommand(message.From.Id, stickerId, notification.Description));

            await botClient.SendTextMessageAsync(message.Chat, "Sticker described");
        }
    }
}
