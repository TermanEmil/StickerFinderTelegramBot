using System;
using System.Collections.Generic;
using System.Linq;
using Domain;
using Newtonsoft.Json;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.Callbacks.RemoveDescription;

namespace TelegramBot
{
    public static class StickerDescriptionButtonsBuilder
    {
        public static InlineKeyboardMarkup BuildDescriptionMarkup(IEnumerable<StickerDescription> descriptions)
        {
            if (descriptions is null)
                throw new ArgumentNullException(nameof(descriptions));

            // A button on each line
            return new InlineKeyboardMarkup(BuildDescriptionsButtons(descriptions).Select(x => new[] { x }));
        }

        private static IEnumerable<InlineKeyboardButton> BuildDescriptionsButtons(IEnumerable<StickerDescription> descriptions)
        {
            return descriptions.Select(description => new InlineKeyboardButton
            {
                Text = $"❌ {description.Description}",
                CallbackData = JsonConvert.SerializeObject(new RemoveDescriptionCallbackData(description.Id)),
            });
        }
    }
}
