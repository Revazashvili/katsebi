using API.Schema;
using Database;
using EntityGraphQL.AspNet;
using GraphQL.Server.Ui.Altair;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDatabase(options => options.UseInMemoryDatabase = true);
builder.Services.AddGraphQLSchema(SchemaOptions.AddGraphQlOptions);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => options.EnableAnnotations());

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();
app.UseEndpoints(routeBuilder =>
{
    routeBuilder.MapControllers();
    routeBuilder.MapGraphQL<KatsebiContext>();
    routeBuilder.MapGraphQLAltair(new AltairOptions
    {
        GraphQLEndPoint = PathString.FromUriComponent("/graphql")
    });
});
app.Run();