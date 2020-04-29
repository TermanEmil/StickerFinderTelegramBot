using System;
using MediatR;
using Telegram.Bot.Types;

namespace TelegramBot.Commands.OnDescribeSticker
{
    public class OnDescribeStickerCommand : INotification
    {
        public OnDescribeStickerCommand(Message message, string description)
        {
            Message = message ?? throw new ArgumentNullException(nameof(message));
            Description = description ?? throw new ArgumentNullException(nameof(description));
        }

        public Message Message { get; }
        public string Description { get; }
    }
}
