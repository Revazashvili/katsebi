using API.Schema;
using EntityGraphQL.AspNet;
using GraphQL.Server.Ui.Altair;

var builder = WebApplication.CreateBuilder(args);

const string connectionString = "CONNECTION_STRING";

builder.Services
    .AddDatabase(builder.Configuration.GetValue<string>(connectionString))
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