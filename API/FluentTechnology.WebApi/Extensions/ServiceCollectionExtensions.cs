using FluentTechnology.Application.Interfaces;
using FluentTechnology.Application.Services;

namespace FluentTechnology.WebApi.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationLayerServices(this IServiceCollection services)
    {
        // Register the LookupService
        services.AddScoped<ILookupService, LookupService>();
        services.AddScoped<IUserRegistrationService, UserRegistrationService>();

        // Add registrations for other application layer services as needed

        return services;
    }
}
