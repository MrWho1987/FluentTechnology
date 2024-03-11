using FluentTechnology.Domain.Entities;

namespace FluentTechnology.Domain.Interfaces;

public interface IPreferredCommunicationMethodsRepository
{
    Task<IEnumerable<PreferredCommunicationMethods>> GetAllAsync();
    Task<PreferredCommunicationMethods?> GetByIdAsync(Guid id);
    Task<PreferredCommunicationMethods> AddAsync(PreferredCommunicationMethods preferredCommunicationMethod);
    Task UpdateAsync(PreferredCommunicationMethods preferredCommunicationMethod);
    Task DeleteAsync(Guid id);
}
