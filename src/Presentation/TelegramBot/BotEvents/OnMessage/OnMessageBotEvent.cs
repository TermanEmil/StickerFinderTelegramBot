using System;
using Telegram.Bot.Types;

namespace TelegramBot.BotEvents.OnMessage
{
    public class OnMessageBotEvent : IBotEvent
    {
        public OnMessageBotEvent(Message message)
        {
            Message = message ?? throw new ArgumentNullException(nameof(message));
        }

        public Message Message { get; }
    }
}
