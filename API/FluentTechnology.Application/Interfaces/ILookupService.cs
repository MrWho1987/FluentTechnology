using FluentTechnology.Application.DTOs;
namespace FluentTechnology.Application.Interfaces
{
    public interface ILookupService
    {
        Task<IEnumerable<LookupDto>> GetPreferredCommunicationMethodsAsync();
        Task<IEnumerable<LookupDto>> GetOrganizationTypesAsync();
        Task<IEnumerable<LookupDto>> GetGrantCategoriesAsync();
        Task<IEnumerable<LookupDto>> GetPersonalizedContentPreferencesAsync();
    }
}
