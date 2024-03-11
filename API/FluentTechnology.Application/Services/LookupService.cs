using FluentTechnology.Application.DTOs;
using FluentTechnology.Application.Interfaces;
using FluentTechnology.Domain.Interfaces;

namespace FluentTechnology.Application.Services
{
    public class LookupService : ILookupService
    {
        private readonly IPreferredCommunicationMethodsRepository _communicationMethodsRepository;
        private readonly IOrganizationTypesRepository _organizationTypesRepository;
        private readonly IGrantCategoriesRepository _grantCategoriesRepository;
        private readonly IPersonalizedContentPreferencesRepository _contentPreferencesRepository;

        public LookupService(
            IPreferredCommunicationMethodsRepository communicationMethodsRepository,
            IOrganizationTypesRepository organizationTypesRepository,
            IGrantCategoriesRepository grantCategoriesRepository,
            IPersonalizedContentPreferencesRepository contentPreferencesRepository)
        {
            _communicationMethodsRepository = communicationMethodsRepository;
            _organizationTypesRepository = organizationTypesRepository;
            _grantCategoriesRepository = grantCategoriesRepository;
            _contentPreferencesRepository = contentPreferencesRepository;
        }

        public async Task<IEnumerable<LookupDto>> GetPreferredCommunicationMethodsAsync()
        {
            var methods = await _communicationMethodsRepository.GetAllAsync();
            return methods.Select(m => new LookupDto { Id = m.Id, Name = m.Method });
        }

        public async Task<IEnumerable<LookupDto>> GetOrganizationTypesAsync()
        {
            var types = await _organizationTypesRepository.GetAllAsync();
            return types.Select(t => new LookupDto { Id = t.Id, Name = t.TypeName });
        }

        public async Task<IEnumerable<LookupDto>> GetGrantCategoriesAsync()
        {
            var categories = await _grantCategoriesRepository.GetAllAsync();
            return categories.Select(c => new LookupDto { Id = c.Id, Name = c.CategoryName });
        }

        public async Task<IEnumerable<LookupDto>> GetPersonalizedContentPreferencesAsync()
        {
            var preferences = await _contentPreferencesRepository.GetAllAsync();
            return preferences.Select(p => new LookupDto { Id = p.Id, Name = p.PreferenceName });
        }
    }
}
