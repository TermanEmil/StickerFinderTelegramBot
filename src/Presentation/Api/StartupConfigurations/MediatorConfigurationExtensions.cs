using Application;
using Application.DescribeSticker;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using TelegramBot.BotEvents.OnMessage;
using Utilities;

namespace Api.StartupConfigurations
{
    public static class MediatorConfigurationExtensions
    {
        public static void ConfigureMediator(this IServiceCollection services)
        {
            services.AddMediatR(
                typeof(DescribeStickerCommandHandler),
                typeof(OnMessageBotEventHandler),
                typeof(RequestValidationBehavior<,>));

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
        }
    }
}
