using System.Threading;
using System.Threading.Tasks;
using Application;
using MediatR;
using Telegram.Bot;

namespace TelegramBot.Callbacks.RemoveDescription
{
    public class RemoveDescriptionCallbackHandler : INotificationHandler<RemoveDescriptionCallback>
    {
        private readonly IMediator mediator;
        private readonly ITelegramBotClient botClient;

        public RemoveDescriptionCallbackHandler(IMediator mediator, ITelegramBotClient botClient)
        {
            this.mediator = mediator;
            this.botClient = botClient;
        }

        public async Task Handle(RemoveDescriptionCallback notification, CancellationToken ct)
        {
            var callback = notification.CallbackQuery;
            var descriptionId = notification.CallbackData.DescriptionId;

            var description = await mediator.Send(new GetStickerDescriptionQuery(descriptionId), ct);
            await mediator.Send(new RemoveStickerDescriptionCommand(descriptionId), ct);
            var descriptions = await mediator.Send(new GetStickerDescriptionsQuery(description.Sticker.Id), ct);

            await botClient.EditMessageReplyMarkupAsync(
                chatId: callback.Message.Chat,
                messageId: callback.Message.MessageId,
                replyMarkup: StickerDescriptionButtonsBuilder.BuildDescriptionMarkup(descriptions),
                cancellationToken: ct);
        }
    }
}