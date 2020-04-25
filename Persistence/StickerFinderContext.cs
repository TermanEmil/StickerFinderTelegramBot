using DataAccess;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class StickerFinderContext : DbContext, IStickerFinderContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Sticker> Stickers { get; set; }
        public DbSet<StickerDescription> StickerDescriptions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite("Data Source=StickerFinderContext.db");
        }
    }
}
