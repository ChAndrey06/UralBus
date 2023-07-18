using System.Reflection;
using Common.Attributes;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Interfaces;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace PL.CrudTesting.API.Swagger;


//@TODO: Fix this
public class CanPatchSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        // if (schema.Properties == null || !schema.Properties.Any())
        //     return;

        var patchProperties = context.Type.GetProperties().Where(p => p.GetCustomAttribute<CanPatchAttribute>() != null);

        foreach (var property in patchProperties)
        {
            var propertyName = property.Name;//.ToCamelCase();
            if (schema.Properties.ContainsKey(propertyName))
            {
                var propertySchema = schema.Properties[propertyName];
                if (propertySchema.Example != null)
                {
                    schema.Properties[propertyName].Example = new OpenApiString(property.PropertyType.GetDefaultValue().ToString());
                }
                else
                {
                    schema.Properties[propertyName].Example = new OpenApiString(property.PropertyType.GetDefaultValue().ToString());
                }
            }
        }
    }
}






public static class TypeExtensions
{
    public static bool IsNullable(this Type type)
    {
        return Nullable.GetUnderlyingType(type) != null;
    }
}