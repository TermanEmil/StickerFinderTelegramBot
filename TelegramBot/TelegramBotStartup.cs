using System;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot;
using TelegramBot.BotEvents;

namespace TelegramBot
{
    public static class TelegramBotStartup
    {
        public static void ConfigureTelegramBot(this IServiceCollection services, string botToken)
        {
            services.AddSingleton(new TelegramBotClient(botToken));
        }

        public static void ConfigureTelegramBot(this IServiceProvider serviceProvider, TelegramBotClient botClient)
        {
            botClient.OnMessage += async (s, e) =>
            {
                using var scope = serviceProvider.CreateScope();
                var mediator = scope.ServiceProvider.GetService<IMediator>();
                await mediator.Publish(new OnMessage(e.Message));
            };

            botClient.OnInlineQuery += async (s, e) =>
            {
                using var scope = serviceProvider.CreateScope();
                var mediator = scope.ServiceProvider.GetService<IMediator>();
                await mediator.Publish(new OnInlineQuery(e.InlineQuery));
            };
            botClient.StartReceiving();
        }
    }
}
