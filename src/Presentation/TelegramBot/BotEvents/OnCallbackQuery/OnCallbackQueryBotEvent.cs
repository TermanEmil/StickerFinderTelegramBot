using System;
using Telegram.Bot.Types;

namespace TelegramBot.BotEvents.OnCallbackQuery
{
    public class OnCallbackQueryBotEvent : IBotEvent
    {
        public OnCallbackQueryBotEvent(CallbackQuery callbackQuery)
        {
            CallbackQuery = callbackQuery ?? throw new ArgumentNullException(nameof(callbackQuery));
        }

        public CallbackQuery CallbackQuery { get; }
    }
}
