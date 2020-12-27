using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DnD10k.Extensions;
using DnD10k.Helpers;
using DnD10k.V1.Services;
using DnD10k.V1.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DnD10k
{
    public class Startup
    {
        private bool _development = EnvironmentHelper.IsDevelopment();

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddApiVersioningAndSwagger(
                "DnD10k API",
                "Swagger Visualization for the Dungeons and Dragons 10k Effects Endpoints.");

            services
                .AddSingleton<IDBService, DBProvider>();

            services
                .AddScoped<IGenerationService, GenerationProvider>();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) { app.UseDeveloperExceptionPage(); }

            app.UseRouting();

            if (_development)
            {
                app.UseCors(options =>
                    options
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    );
            }
            else
            {
                app.UseCors();
            }

            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.UseAuthentication();

            //app.Use(async (context, next) =>
            //{
            //    await next();
            //    if (context.Response.StatusCode == 404
            //            && !Path.HasExtension(context.Request.Path.Value)
            //            && !context.Request.Path.Value.StartsWith("/api"))
            //    {
            //        context.Request.Path = "/Index.html";
            //        context.Response.StatusCode = 200;
            //        await next();
            //    }
            //});

            app.UseApiVersioningAndSwagger();

            var headers = new ForwardedHeadersOptions() { ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto };
            app.UseForwardedHeaders(headers);

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}

