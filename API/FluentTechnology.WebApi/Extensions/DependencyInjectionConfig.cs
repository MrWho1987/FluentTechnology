using FluentTechnology.Infrastructure.Repositories;
using FluentTechnology.Domain.Interfaces;

namespace FluentTechnology.WebApi.Extensions;

public static class DependencyInjectionConfig
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Repository registrations
        services.AddScoped<IUsersRepository, UsersRepository>();
        services.AddScoped<IPreferredCommunicationMethodsRepository, PreferredCommunicationMethodsRepository>();
        services.AddScoped<IPersonalizedContentPreferencesRepository, PersonalizedContentPreferencesRepository>();
        services.AddScoped<IOrganizationTypesRepository, OrganizationTypesRepository>();
        services.AddScoped<IGrantCategoriesRepository, GrantCategoriesRepository>();

        // Add other application services, infrastructure services, etc.

        return services;
    }
}
