using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class StickerDescriptionConfiguration : IEntityTypeConfiguration<StickerDescription>
    {
        public void Configure(EntityTypeBuilder<StickerDescription> builder)
        {
            builder
                .HasOne(x => x.Author)
                .WithMany()
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(x => x.Sticker)
                .WithMany()
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .Property(x => x.Description)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(128);
        }
    }
}
