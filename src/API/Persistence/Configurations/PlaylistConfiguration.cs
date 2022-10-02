using API.Persistence.Entities;

namespace API.Persistence.Configurations;

public class PlaylistConfiguration : IEntityTypeConfiguration<Playlist>
{
    public void Configure(EntityTypeBuilder<Playlist> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Property(x => x.Name).HasMaxLength(100).IsRequired().IsUnicode();
        builder.HasIndex(x => x.Name);

        builder.HasMany(x => x.Episodes)
            .WithOne(x => x.Playlist)
            .HasForeignKey(x => x.PlaylistId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}