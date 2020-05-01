using System;
using MediatR;
using Telegram.Bot.Types;

namespace TelegramBot.Commands.BotListDescriptions
{
    public class BotListDescriptionsCommand : INotification
    {
        public Message Message { get; }

        public BotListDescriptionsCommand(Message message)
        {
            Message = message ?? throw new ArgumentNullException(nameof(message));
        }
    }
}
