using EntityGraphQL.AspNet;

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