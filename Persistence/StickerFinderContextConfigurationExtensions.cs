using System;
using DataAccess;
using Microsoft.Extensions.DependencyInjection;

namespace Persistence
{
    public static class StickerFinderContextConfigurationExtensions
    {
        public static void ConfigureDbContext(this IServiceCollection services)
        {
            services
                .AddEntityFrameworkSqlite()
                .AddDbContext<IStickerFinderContext, StickerFinderContext>();
        }

        public static void ConfigureDbContext(this IServiceProvider _)
        {
            using var context = new StickerFinderContext();
            context.Database.EnsureCreated();
        }
    }
}
