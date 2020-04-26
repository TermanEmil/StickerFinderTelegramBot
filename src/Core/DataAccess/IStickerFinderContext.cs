using Domain;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public interface IStickerFinderContext : IDbContext
    {
        DbSet<User> Users { get; }
        DbSet<Sticker> Stickers { get; }
        DbSet<StickerDescription> StickerDescriptions { get; }
    }
}
