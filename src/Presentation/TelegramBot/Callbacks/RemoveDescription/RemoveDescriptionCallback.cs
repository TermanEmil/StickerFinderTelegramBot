using System;
using MediatR;
using Telegram.Bot.Types;

namespace TelegramBot.Callbacks.RemoveDescription
{
    public class RemoveDescriptionCallback : INotification
    {
        public RemoveDescriptionCallback(CallbackQuery callbackQuery, RemoveDescriptionCallbackData callbackData)
        {
            CallbackQuery = callbackQuery ?? throw new ArgumentNullException(nameof(callbackQuery));
            CallbackData = callbackData ?? throw new ArgumentNullException(nameof(callbackData));
        }

        public CallbackQuery CallbackQuery { get; }
        public RemoveDescriptionCallbackData CallbackData { get; }
    }
}
