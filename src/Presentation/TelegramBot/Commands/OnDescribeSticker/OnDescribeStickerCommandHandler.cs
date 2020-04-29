﻿using System.Threading;
using System.Threading.Tasks;
using Application;
using MediatR;
using Telegram.Bot;

namespace TelegramBot.Commands.OnDescribeSticker
{
    public class OnDescribeStickerCommandHandler : INotificationHandler<OnDescribeStickerCommand>
    {
        private readonly IMediator mediator;
        private readonly ITelegramBotClient botClient;

        public OnDescribeStickerCommandHandler(IMediator mediator, ITelegramBotClient botClient)
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