using System;
using MediatR;
using Telegram.Bot.Types;

namespace TelegramBot.Commands.BotListHelp
{
    public class BotListHelpCommand : INotification
    {
        public BotListHelpCommand(Message message)
        {
            Message = message ?? throw new ArgumentNullException(nameof(message));
        }

        public Message Message { get; }
    }
}
