using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application;
using MediatR;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using Utilities;

namespace TelegramBot.Commands.OnListDescriptions
{
    public class OnListDescriptionsCommandHandler : INotificationHandler<OnListDescriptionsCommand>
    {
        private readonly IMediator mediator;
        private readonly ITelegramBotClient botClient;

        public OnListDescriptionsCommandHandler(IMediator mediator, ITelegramBotClient botClient)
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

            var sticker = message.ReplyToMessage.Sticker;
            var fromId = message.From.Id.ToString();

            await mediator.Send(new EnsureStickerIsRegisteredCommand(sticker.FileUniqueId, sticker.FileId), ct);
            await mediator.Send(new EnsureUserIsRegisteredCommand(fromId), ct);
            var descriptions = await mediator.Send(new GetStickerDescriptionsQuery(sticker.FileUniqueId), ct);

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