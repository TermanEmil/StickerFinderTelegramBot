using System;
using Api.StartupConfigurations;
using Application.DescribeSticker;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot;
using TelegramBot;

namespace Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<DescribeStickerCommand>();

            services.ConfigureMediator();
            services.ConfigureDbContext(Configuration.GetConnectionString("StickerFinderDb"));
            services.ConfigureTelegramBot(Configuration["TelegramBot:Token"]);
        }

        public void Configure(IApplicationBuilder app, ITelegramBotClient botClient)
        {
            app.ApplicationServices.ConfigureTelegramBot(botClient);
        }
    }
}
