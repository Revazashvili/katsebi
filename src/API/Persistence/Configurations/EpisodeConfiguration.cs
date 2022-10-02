using API.Persistence.Entities;

namespace API.Persistence.Configurations;

public class EpisodeConfiguration : IEntityTypeConfiguration<Episode>
{
    public void Configure(EntityTypeBuilder<Episode> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Property(x => x.Name).HasMaxLength(250).IsRequired().IsUnicode();
        builder.HasIndex(x => x.Name);
        builder.Property(x => x.EpisodeNumber).IsRequired(false);
        builder.Property(x => x.YoutubeUrl).HasMaxLength(500).IsRequired();
        builder.Property(x => x.UploadTime).IsRequired();

        builder.HasOne(x => x.Playlist)
            .WithMany(x => x.Episodes)
            .HasForeignKey(x => x.PlaylistId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.Guests)
            .WithMany(x => x.Episodes);

        builder.HasMany(x => x.Quotes)
            .WithOne(x => x.Episode)
            .HasForeignKey(x => x.EpisodeId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}