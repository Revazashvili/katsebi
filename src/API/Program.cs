using API.Schema;
using EntityGraphQL.AspNet;
using GraphQL.Server.Ui.Altair;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddDatabase(options => options.UseInMemoryDatabase = true)
    .AddGraphQLSchema(SchemaOptions.AddGraphQlOptions);

var app = builder.Build();

app.UseHttpsRedirection();

app.UseRouting();
app.UseEndpoints(routeBuilder =>
{
    routeBuilder.MapGraphQL<KatsebiContext>();
    routeBuilder.MapGraphQLAltair(new AltairOptions
    {
        GraphQLEndPoint = PathString.FromUriComponent("/graphql")
    });
});

await app.RunAsync();