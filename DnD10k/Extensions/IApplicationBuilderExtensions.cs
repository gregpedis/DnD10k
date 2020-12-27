using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnD10k.Extensions
{
    public static class IApplicationBuilderExtensions
    {
        /// <summary>
        /// Execute all the neccessary configuration for providing versioning and swagger files.
        /// </summary>
        public static IApplicationBuilder UseApiVersioningAndSwagger(this IApplicationBuilder application)
        {
            application
                .UseSwagger()
                .UseSwaggerUI(options =>
                {
                    var provider = (IApiVersionDescriptionProvider)application
                        .ApplicationServices
                        .GetService(typeof(IApiVersionDescriptionProvider));

                    // build a swagger endpoint for each discovered API version
                    foreach (var description in provider.ApiVersionDescriptions)
                    {
                        options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                    }
                });

            return application;
        }
    }
}

