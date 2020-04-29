using System;
using MediatR;
using Telegram.Bot.Types;

namespace TelegramBot.Commands.OnListDescriptions
{
    public class OnListDescriptionsCommand : INotification
    {
        public Message Message { get; }

        public OnListDescriptionsCommand(Message message)
        {
            Message = message ?? throw new ArgumentNullException(nameof(message));
        }
    }
}
