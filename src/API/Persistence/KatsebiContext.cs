using System.Reflection;
using Database.Entities;

namespace Database;

#pragma warning disable CS8618
public class KatsebiContext : DbContext
{
    public KatsebiContext(DbContextOptions<KatsebiContext> options) : base(options) { }
    
    public DbSet<Playlist> Playlists { get; set; }
    public DbSet<Episode> Episodes { get; set; }
    public DbSet<Guest> Guests { get; set; }
    public DbSet<Quote> Quotes { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasDefaultSchema("katsebi");
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }
}