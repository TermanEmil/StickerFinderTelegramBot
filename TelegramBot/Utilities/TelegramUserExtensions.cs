using System;
using Telegram.Bot.Types;

namespace TelegramBot.Utilities
{
    public static class TelegramUserExtensions
    {
        public static string FullName(this User user)
        {
            if (user is null)
                throw new ArgumentNullException(nameof(user));

            var name = $"{user.FirstName} {user.LastName}";
            if (user.Username != null)
                name += $" {user.Username}";

            return name;
        }
    }
}
