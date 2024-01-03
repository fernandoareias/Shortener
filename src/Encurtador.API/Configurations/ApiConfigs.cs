using System.Text;
using Encurtador.API.Data;
using Encurtador.API.Data.Repositories;
using Encurtador.API.Data.Repositories.Interfaces;
using Encurtador.API.Services;
using Encurtador.API.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.IdentityModel.Tokens;
using Prometheus;
using Serilog;
using Shortener.API.Middlewares;

namespace Encurtador.API.Configurations;

public static class ApiConfigs
{
    public  static void ApiConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        
        services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

        services.AddEndpointsApiExplorer();
        services.UseSwagger();
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = configuration.GetConnectionString("RedisConnection");
        });

      


        var key = Encoding.ASCII.GetBytes(Settings.Secret);
        services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        
        ApiInjection(services);
    }

    public static void UseApiConfiguration(this WebApplication app)
    {
        app.UseSerilogRequestLogging();
        app.UseMiddleware<RequestContextLoggingMiddleware>();
        var counter = Metrics.CreateCounter("webapimetrics", "Count requests endpoints",
            new CounterConfiguration
            {
                LabelNames = new[] { "method", "endpoint" }
            });
        
        var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

        app.Use((context, next) =>
        {
            counter.WithLabels(context.Request.Method, context.Request.Path).Inc();
            return next();
        });


        app.UseMetricServer(settings => settings.EnableOpenMetrics = false);
        app.UseHttpMetrics();

        app.UseMySwagger(provider);

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

    }
    
    private static void ApiInjection(this IServiceCollection services)
    {
        services.AddScoped<IMongoContext, MongoContext>();
        services.AddScoped<IUnitOfWork, MongoContext>();
        services.AddScoped<IShortenerRepository, ShortenerRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ICompanyRepository, CompanyRepository>();
        services.AddScoped<IAuthenticationServices, AuthenticationServices>();
        services.AddScoped<ICompanyServices, CompanyServices>();
        services.AddScoped<IShortenerServices, ShortenerServices>();
    }
}