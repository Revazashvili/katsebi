using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Database.UnitTests;

public class ServiceCollectionExtensionsTests
{
    private const string PostgresConnectionString = "Server=localhost;Port=5432;User Id=postgres;Password=mysecretpassword;Database=test;";
    [Fact]
    public void Should_Throw_Exception()
    {
        var services = new ServiceCollection();
        Assert.Throws<Exception>(() => services.AddDatabase(_ => { }));
    }
    
    [Fact]
    public void Should_Add_InMemory_Database()
    {
        var services = new ServiceCollection();
        services.AddDatabase(options => options.UseInMemoryDatabase = true);

        var serviceProvider = services.BuildServiceProvider();
        var katsebiContext = serviceProvider.GetService<KatsebiContext>()!;
        
        Assert.True(katsebiContext.Database.IsInMemory());
    }

    [Fact]
    public void Should_Add_Postgres_Database()
    {
        var services = new ServiceCollection();
        services.AddDatabase(options => options.ConnectionString = PostgresConnectionString);
        
        var serviceProvider = services.BuildServiceProvider();
        var katsebiContext = serviceProvider.GetService<KatsebiContext>()!;

        Assert.True(katsebiContext.Database.IsNpgsql());
    }
    
    [Fact]
    public void Should_Add_Postgres_Database_And_Migrate()
    {
        var services = new ServiceCollection();
        services.AddDatabase(options =>
        {
            options.ConnectionString = PostgresConnectionString;
            options.EnableAutoMigration = true;
        });
        
        var serviceProvider = services.BuildServiceProvider();
        var katsebiContext = serviceProvider.GetService<KatsebiContext>()!;
        Assert.True(katsebiContext.Database.IsNpgsql());
        
        var ensureDeleted = katsebiContext.Database.EnsureDeleted();
        Assert.True(ensureDeleted);
        var ensureCreated = katsebiContext.Database.EnsureCreated();
        Assert.True(ensureCreated);
    }
}