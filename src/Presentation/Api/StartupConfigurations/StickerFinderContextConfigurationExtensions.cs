using DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistence;

namespace Api.StartupConfigurations
{
    public static class StickerFinderContextConfigurationExtensions
    {
        public static void ConfigureDbContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<StickerFinderDbContext>(options => options.UseSqlServer(connectionString));
            services.AddScoped<IStickerFinderDbContext, StickerFinderDbContext>();
        }
    }
}
