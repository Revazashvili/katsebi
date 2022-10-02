namespace API.Persistence;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, string connectionString) =>
        services.AddDbContextPool<KatsebiContext>(builder =>
        {
            builder.UseSqlite(connectionString, optionsBuilder =>
            {
                optionsBuilder.MigrationsAssembly(typeof(KatsebiContext).Assembly.FullName);
                optionsBuilder.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
            });
        }).AutoMigrate();

    private static IServiceCollection AutoMigrate(this IServiceCollection services)
    {
        var serviceProvider = services.BuildServiceProvider();
        var context = serviceProvider.GetService<KatsebiContext>();
        context!.Database.Migrate();
        return services;
    }
}