using System.Threading;
using System.Threading.Tasks;
using Application;
using Application.DescribeSticker;
using MediatR;
using TelegramBot.Commands.BotListDescriptions;
using Utilities.Exceptions;

namespace TelegramBot.Commands.BotDescribeSticker
{
    public class BotDescribeStickerCommandHandler : INotificationHandler<BotDescribeStickerCommand>
    {
        private readonly IMediator mediator;

        public BotDescribeStickerCommandHandler(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task Handle(BotDescribeStickerCommand notification, CancellationToken ct)
        {
            var message = notification.Message;

            if (message.ReplyToMessage?.Sticker is null)
                throw new ValidationException("Must be a reply to a sticker");

            var sticker = message.ReplyToMessage.Sticker;
            var fromId = message.From.Id.ToString();

            await mediator.Send(new EnsureStickerIsRegisteredCommand(sticker.FileUniqueId, sticker.FileId), ct);
            await mediator.Send(new EnsureUserIsRegisteredCommand(fromId), ct);
            await mediator.Send(new DescribeStickerCommand(fromId, sticker.FileUniqueId, notification.Description), ct);

            await mediator.Publish(new BotListDescriptionsCommand(message), ct);
        }
    }
}