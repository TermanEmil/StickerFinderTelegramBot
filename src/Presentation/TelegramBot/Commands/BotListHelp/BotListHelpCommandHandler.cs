using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace TelegramBot.Commands.BotListHelp
{
    public class BotListHelpCommandHandler : INotificationHandler<BotListHelpCommand>
    {
        private readonly ITelegramBotClient botClient;

        public BotListHelpCommandHandler(ITelegramBotClient botClient)
        {
            this.botClient = botClient;
        }

        public Task Handle(BotListHelpCommand notification, CancellationToken ct)
        {
            return botClient.SendTextMessageAsync(
                chatId: notification.Message.Chat,
                text: BuildHelpMessage(),
                parseMode: ParseMode.Html,
                cancellationToken: ct);
        }

        public static string BuildHelpMessage()
        {
            return new StringBuilder()
                .AppendLine("Find stickers faster using sticker descriptions.")
                .AppendLine("/describe - <i>to describe stickers</i>")
                .AppendLine("  1. Send this bot a sticker")
                .AppendLine("  2. Reply to it with some text:")
                .AppendLine("    /describe Elon Tusk")
                .AppendLine("  3. Done")
                .AppendLine()
                .AppendLine("/list - <i>to list the available descriptions of a sticker</i>")
                .AppendLine()
                .AppendLine("<b>To find stickers</b> - <i>call this bot using the inline functionality:</i>")
                .AppendLine("  <i>@FindMyStickerBot Elon</i>")
                .ToString();
        }
    }
}