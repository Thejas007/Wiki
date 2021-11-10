
namespace SwaggerFilters
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http.Description;
    using Swashbuckle.Swagger;

    /// <summary>
    /// JsonPatchDocumentFilter.
    /// </summary>
    /// <seealso cref="Swashbuckle.Swagger.IDocumentFilter" />
    public class JsonPatchDocumentFilter : IDocumentFilter
    {
        /// <summary>
        /// Applies the specified swagger document.
        /// </summary>
        /// <param name="swaggerDoc">The swagger document.</param>
        /// <param name="schemaRegistry">The schema registry.</param>
        /// <param name="apiExplorer">The API explorer.</param>
        public void Apply(SwaggerDocument swaggerDoc, SchemaRegistry schemaRegistry, IApiExplorer apiExplorer)
        {
            var schemas = swaggerDoc.definitions.ToList();
            foreach (var item in schemas)
            {
                if (item.Key.Contains("PatchOperation") || item.Key.StartsWith("JsonPatchDocument"))
                {
                    swaggerDoc.schemes.Remove(item.Key);
                }
            }

            swaggerDoc.definitions.Add("PatchOperation",
                new Schema
                {
                    type = "object",
                    properties = new Dictionary<string, Schema>
                    {
                        { "op", new Schema { type = "string", example = "replace" } },
                        { "value", new Schema { type = "string", example = "propertyValue" } },
                        { "path", new Schema { type = "string", example = "/propertyName" } }
                    }
                });

            swaggerDoc.definitions.Add("JsonPatchDocument",
                new Schema
                {
                    type = "array",
                    items = new Schema
                    {
                        @ref = "#/definitions/PatchOperation"
                    },
                    description = "Array of operations to perform",
                });

            foreach (var path in swaggerDoc.paths.Select(p => p.Value.patch))
            {
                if (path?.parameters != null)
                {
                    foreach (var item in path.parameters.Where(c => c.@in == "body" && c?.schema?.@ref?.ToLowerInvariant().Contains("patch") == true))
                    {
                        item.schema.@ref = "#/definitions/JsonPatchDocument";
                    }
                }
            }
        }
    }
}
