namespace API.Persistence;

public static class ServiceCollectionExtensions
{
    private const string InMemoryDatabaseName = "KatsebiDb";
    public static IServiceCollection AddDatabase(this IServiceCollection services,Action<DatabaseOptions> databaseOptionsAction)
    {
        var databaseOptions = new DatabaseOptions();
        databaseOptionsAction.Invoke(databaseOptions);
        services = databaseOptions switch
        {
            {ConnectionString: var cs, UseInMemoryDatabase: false} when string.IsNullOrEmpty(cs) =>
                throw new Exception("One of property must have value"),
            { UseInMemoryDatabase: true} => 
                services.AddDbContext<KatsebiContext>(options => options.UseInMemoryDatabase(InMemoryDatabaseName)),
            { UseInMemoryDatabase:false,ConnectionString: var cs} =>
                services.AddDbContext<KatsebiContext>(options =>
                {
                    options.UseNpgsql(databaseOptions.ConnectionString!,
                        builder =>
                        {
                            builder.MigrationsAssembly(typeof(KatsebiContext).Assembly.FullName);
                            builder.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                        });
                })
        };

        return databaseOptions.EnableAutoMigration ? services.AutoMigrate() : services;
    }

    private static IServiceCollection AutoMigrate(this IServiceCollection services)
    {
        var serviceProvider = services.BuildServiceProvider();
        var context = serviceProvider.GetService<KatsebiContext>();
        if (context!.Database.IsNpgsql())
            context.Database.Migrate();
        return services;
    }
}