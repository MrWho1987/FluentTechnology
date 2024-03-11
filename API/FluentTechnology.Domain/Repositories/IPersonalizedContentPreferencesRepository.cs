using FluentTechnology.Domain.Entities;

namespace FluentTechnology.Domain.Interfaces;

public interface IPersonalizedContentPreferencesRepository
{
    Task<IEnumerable<PersonalizedContentPreferences>> GetAllAsync();
    Task<PersonalizedContentPreferences?> GetByIdAsync(Guid id);
    Task<PersonalizedContentPreferences> AddAsync(PersonalizedContentPreferences personalizedContentPreference);
    Task UpdateAsync(PersonalizedContentPreferences personalizedContentPreference);
    Task DeleteAsync(Guid id);
    Task<IEnumerable<PersonalizedContentPreferences>> GetManyByPreferenceIdsAsync(IEnumerable<Guid> ids);
}
