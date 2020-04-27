using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application;
using MediatR;
using Microsoft.EntityFrameworkCore.Internal;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Utilities;

namespace TelegramBot.BotEvents.Commands
{
    public class OnListDescriptionsCommand : INotification
    {
        public Message Message { get; }

        public OnListDescriptionsCommand(Message message)
        {
            Message = message ?? throw new ArgumentNullException(nameof(message));
        }
    }

    public class OnListDescriptionsCommandHandler : INotificationHandler<OnListDescriptionsCommand>
    {
        private readonly IMediator mediator;
        private readonly TelegramBotClient botClient;

        public OnListDescriptionsCommandHandler(IMediator mediator, TelegramBotClient botClient)
        {
            this.mediator = mediator;
            this.botClient = botClient;
        }

        public async Task Handle(OnListDescriptionsCommand notification, CancellationToken ct)
        {
            var message = notification.Message;

            if (message.ReplyToMessage?.Sticker is null)
            {
                await botClient.SendTextMessageAsync(message.Chat, "Must be a reply to a sticker", cancellationToken: ct);
                return;
            }

            var stickerId = message.ReplyToMessage.Sticker.FileId;
            var fromId = message.From.Id.ToString();

            await mediator.Send(new EnsureStickerIsRegisteredCommand(stickerId), ct);
            await mediator.Send(new EnsureUserIsRegisteredCommand(fromId), ct);
            var descriptions = await mediator.Send(new GetStickerDescriptionsQuery(stickerId), ct);

            var newMessage = BuildMessage(descriptions.ToList());
            await botClient.SendTextMessageAsync(
                message.Chat,
                newMessage,
                ParseMode.Html,
                replyToMessageId: message.MessageId,
                cancellationToken: ct);
        }

        private string BuildMessage(IReadOnlyCollection<string> descriptions)
        {
            if (descriptions.IsEmpty())
                return "<i>This sticker has no descriptions yet</i>";

            return string.Join(Environment.NewLine, descriptions);
        }
    }
}
