using Application;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using TelegramBot.BotEvents.OnMessage;

namespace Api.StartupConfigurations
{
    public static class MediatorConfigurationExtensions
    {
        public static void ConfigureMediator(this IServiceCollection services)
        {
            services.AddMediatR(
                typeof(DescribeStickerCommandHandler),
                typeof(OnMessageBotEventHandler));
        }
    }
}
