using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application;
using MediatR;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.InlineQueryResults;

namespace TelegramBot.BotEvents.OnInlineQuery
{
    public class OnInlineQueryBotEventHandler : INotificationHandler<OnInlineQueryBotEvent>
    {
        private const int MinimumInputLength = 2;

        private readonly IMediator mediator;
        private readonly ITelegramBotClient botClient;

        public OnInlineQueryBotEventHandler(IMediator mediator, ITelegramBotClient botClient)
        {
            this.mediator = mediator;
            this.botClient = botClient;
        }

        public async Task Handle(OnInlineQueryBotEvent notification, CancellationToken ct)
        {
            var query = notification.InlineQuery;
            if (query.Query.Length <= MinimumInputLength)
                return;
            try
            {
                await InnerHandler(query, ct);
            }
            catch (Exception)
            {
                // Ignore for inline queries
            }
        }

        private async Task InnerHandler(InlineQuery query, CancellationToken ct)
        {
            var stickers = await mediator.Send(new FindMatchingStickersQuery(query.Query), ct);
            var results = stickers.Select((s, i) => new InlineQueryResultCachedSticker(i.ToString(), s.FileId));

            await botClient.AnswerInlineQueryAsync(query.Id, results, cancellationToken: ct);
        }
    }
}