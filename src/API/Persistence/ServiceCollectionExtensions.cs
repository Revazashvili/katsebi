namespace API.Persistence;

/// <summary>
/// Contains methods to configure persistence layer.
/// </summary>
internal static class ServiceCollectionExtensions
{
    /// <summary>
    /// Add Persistence layer in IoC Container.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add persistence to.</param>
    /// <param name="connectionString">Database Connection String.</param>
    /// <returns>A reference to this instance after the operation has completed.</returns>
    internal static IServiceCollection AddPersistence(this IServiceCollection services, string connectionString) =>
        services.AddDbContextPool<KatsebiContext>(builder =>
        {
            builder.UseSqlite($"Data Source={connectionString}", optionsBuilder =>
            {
                optionsBuilder.MigrationsAssembly(typeof(KatsebiContext).Assembly.FullName);
                optionsBuilder.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
            });
        }).AutoMigrate();

    /// <summary>
    /// Auto migrates schema changes to the database.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to retrieve <see cref="KatsebiContext"/> instance.</param>
    /// <returns>A reference to this instance after the operation has completed.</returns>
    private static IServiceCollection AutoMigrate(this IServiceCollection services)
    {
        var serviceProvider = services.BuildServiceProvider();
        var context = serviceProvider.GetService<KatsebiContext>();
        context!.Database.Migrate();
        return services;
    }
}