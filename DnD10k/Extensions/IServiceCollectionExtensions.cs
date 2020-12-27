using DnD10k.Base;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnD10k.Extensions
{
    public static class IServiceCollectionExtensions
    {
        /// <summary>
        /// Execute all the neccessary configuration for providing versioning and swagger files.
        /// </summary>
        public static IServiceCollection AddApiVersioningAndSwagger(this IServiceCollection services, string title = "", string description = "")
        {
            services.AddApiVersioning(options => options.ReportApiVersions = true);
            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

            services.AddTransient<IConfigureOptions<SwaggerGenOptions>>(serviceProvider =>
            {
                var provider = serviceProvider.GetService<IApiVersionDescriptionProvider>();
                return new ConfigureOptions<SwaggerGenOptions>(options =>
                {
                    foreach (var versionDescription in provider.ApiVersionDescriptions)
                    {
                        var info = new OpenApiInfo()
                        {
                            Title = title,
                            Version = versionDescription.ApiVersion.ToString(),
                            Description = description,
                            Contact = new OpenApiContact() { Name = "DnD10k Team", Email = "developers@dnd10k.gr" },
                        };

                        if (versionDescription.IsDeprecated)
                        {
                            info.Description += " This API version has been deprecated.";
                        }

                        options.SwaggerDoc(versionDescription.GroupName, info);
                    }
                });
            });

            services.AddSwaggerGen(options => options.OperationFilter<SwaggerDefaultValues>());
            return services;
        }
    }
}
