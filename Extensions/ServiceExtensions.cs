using PrivateEvents.Entities;
using PrivateEvents.Repository;
using PrivateEvents.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace PrivateEvents.Extensions;

public static class ServiceExtensions
{
    public static void ConfigureCORS(this IServiceCollection services)
    {
        services.AddCors(optionsn => 
        {
            optionsn.AddPolicy("CorsPolicy",
                builder => builder.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
            );
        });

    }

    public static void ConfigureSQLServer(this IServiceCollection services,
         IConfiguration configuration)
    {
        services.AddDbContext<RepositoryContext>( opts =>
         opts.UseSqlServer(configuration.GetConnectionString("sqlConnection"))
         );
    }

    public static void ConfigureRepository(this IServiceCollection services)
    {
        services.AddScoped<IRepositoryWrapper,RepositoryWrapper>();
    }
}