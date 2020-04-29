using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application;
using MediatR;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.InlineQueryResults;

namespace TelegramBot.BotEvents
{
    public class OnInlineQuery : INotification
    {
        public OnInlineQuery(InlineQuery inlineQuery)
        {
            InlineQuery = inlineQuery ?? throw new ArgumentNullException(nameof(inlineQuery));
        }

        public InlineQuery InlineQuery { get; }
    }

    public class OnInlineQueryHandler : INotificationHandler<OnInlineQuery>
    {
        private readonly IMediator mediator;
        private readonly ITelegramBotClient botClient;

        public OnInlineQueryHandler(IMediator mediator, ITelegramBotClient botClient)
        {
            this.mediator = mediator;
            this.botClient = botClient;
        }

        public async Task Handle(OnInlineQuery notification, CancellationToken ct)
        {
            var query = notification.InlineQuery;
            if (query.Query.Length <= 2)
                return;

            var stickers = await mediator.Send(new FindMatchingStickersQuery(query.Query), ct);
            var results = stickers.Select((s, i) => new InlineQueryResultCachedSticker(i.ToString(), s.FileId));

            await botClient.AnswerInlineQueryAsync(query.Id, results, cancellationToken: ct);
        }
    }
}
