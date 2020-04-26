using DataAccess;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class StickerFinderDbContext : DbContext, IStickerFinderDbContext
    {
        protected StickerFinderDbContext()
        {
        }

        public StickerFinderDbContext(DbContextOptions<StickerFinderDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Sticker> Stickers { get; set; }
        public DbSet<StickerDescription> StickerDescriptions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(StickerFinderDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
