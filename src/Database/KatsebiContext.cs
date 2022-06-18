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
    
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new ())
    {
        foreach (var entry in ChangeTracker.Entries<Auditable>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.Created = DateTime.Now;
                    entry.Entity.LastModified = DateTime.Now;
                    break;
                case EntityState.Modified:
                    entry.Entity.LastModified = DateTime.Now;
                    break;
            }
        }
        return await base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }
}