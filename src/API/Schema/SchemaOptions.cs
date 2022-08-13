using Database;
using Database.Entities;
using EntityGraphQL.AspNet;
using EntityGraphQL.Schema;

namespace API.Schema;

public static class SchemaOptions
{
    public static Action<AddGraphQLOptions<KatsebiContext>> AddGraphQlOptions => options =>
    {
        options.PreBuildSchemaFromContext = (schema) =>
        {
            schema.AddScalarType<DateOnly>("DateOnly", "Type representing a DateOnly");
        };
    };
}