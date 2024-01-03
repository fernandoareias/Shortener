using System;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Encurtador.API.Configurations
{
    public static class SwaggerConfigs
    {
        public static IServiceCollection UseSwagger(this IServiceCollection services)
        {


            services.AddApiVersioning(p =>
            {
                p.DefaultApiVersion = new ApiVersion(1, 0);
                p.ReportApiVersions = true;
                p.AssumeDefaultVersionWhenUnspecified = true;
            });

            services.AddVersionedApiExplorer(p =>
            {
                p.GroupNameFormat = "'v'VVV";
                p.SubstituteApiVersionInUrl = true;
            });

            services.AddSwaggerGen(opt =>
            {

                opt.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Version = "v1",
                    Title = "Shortener API",
                    Description = "Shortner API", 
                    Contact = new OpenApiContact
                    {
                        Name = "Fernando Areias",
                        Email = "nando.calheirosx@gmail.com",
                        Url = new Uri("https://github.com/fernandoareias")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "MIT",
                        Url = new Uri("https://creativecommons.org/licenses/by/4.0")
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                opt.IncludeXmlComments(xmlPath);
            });

            return services;
        }


        public static IApplicationBuilder UseMySwagger(this IApplicationBuilder app, IApiVersionDescriptionProvider provider)
        {
            app.UseApiVersioning();

            app.UseSwagger();

            app.UseSwaggerUI(options =>
            {
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint(
                    $"/swagger/{description.GroupName}/swagger.json",
                    description.GroupName.ToUpperInvariant());
                }
                options.DocExpansion(DocExpansion.List);
            });

            return app;
        }
    }
}

