using API.Persistence.Entities;

namespace API.Persistence.Configurations;

public class GuestConfiguration : IEntityTypeConfiguration<Guest>
{
    public void Configure(EntityTypeBuilder<Guest> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Property(x => x.Name).HasMaxLength(100).IsRequired().IsUnicode();
        builder.HasIndex(x => x.Name);
        builder.Property(x => x.Description).HasMaxLength(500).IsRequired(false).IsUnicode();

        builder.HasMany(x => x.Episodes)
            .WithMany(x => x.Guests);
    }
}