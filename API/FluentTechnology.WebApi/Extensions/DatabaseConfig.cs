using Microsoft.EntityFrameworkCore;
using FluentTechnology.Infrastructure.Data;

namespace FluentTechnology.WebApi.Extensions;

public static class DatabaseConfig
{
    public static IServiceCollection AddDatabaseConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        // Retrieve the connection string from appsettings.json (or other configuration sources)
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        // Register the ApplicationDbContext with the provided connection string
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(connectionString));

        return services;
    }
}
