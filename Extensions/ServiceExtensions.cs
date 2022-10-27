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
}