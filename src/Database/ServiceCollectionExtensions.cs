using Database.Common;
using Microsoft.Extensions.DependencyInjection;

namespace Database;

public static class ServiceCollectionExtensions
{
    private const string InMemoryDatabaseName = "KatsebiDb";
    public static IServiceCollection AddDatabase(this IServiceCollection services,Action<DatabaseOptions> databaseOptionsAction)
    {
        var databaseOptions = new DatabaseOptions();
        databaseOptionsAction.Invoke(databaseOptions);
        if (!databaseOptions.UseInMemoryDatabase.HasValue && string.IsNullOrEmpty(databaseOptions.ConnectionString))
            throw new Exception("One of property must have value");
        if(databaseOptions.UseInMemoryDatabase.HasValue && databaseOptions.UseInMemoryDatabase.Value)
            services.AddDbContext<KatsebiContext>(options => options.UseInMemoryDatabase(InMemoryDatabaseName));
        else
        {
            services.AddDbContext<KatsebiContext>(options =>
            {
                options.UseNpgsql(databaseOptions.ConnectionString!,
                    builder =>
                    {
                        builder.MigrationsAssembly(typeof(KatsebiContext).Assembly.FullName);
                        builder.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                    });
            });
        }
        return services;
    }
}