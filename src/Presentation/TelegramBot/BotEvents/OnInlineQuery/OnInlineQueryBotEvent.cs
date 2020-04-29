using System;
using MediatR;
using Telegram.Bot.Types;

namespace TelegramBot.BotEvents.OnInlineQuery
{
    public class OnInlineQueryBotEvent : INotification
    {
        public OnInlineQueryBotEvent(InlineQuery inlineQuery)
        {
            InlineQuery = inlineQuery ?? throw new ArgumentNullException(nameof(inlineQuery));
        }

        public InlineQuery InlineQuery { get; }
    }
}