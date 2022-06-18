using Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.Configurations;

public class QuoteConfiguration : IEntityTypeConfiguration<Quote>
{
    public void Configure(EntityTypeBuilder<Quote> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Property(x => x.Text).IsRequired().IsUnicode();
        builder.HasIndex(x => x.Text);
        builder.Property(x => x.Description).HasMaxLength(1000).IsRequired(false).IsUnicode();
        builder.Property(x => x.Author).HasMaxLength(100).IsRequired().IsUnicode();

        builder.HasOne(x => x.Episode)
            .WithMany(x => x.Quotes)
            .HasForeignKey(x => x.EpisodeId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}