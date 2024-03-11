using FluentTechnology.Domain.Entities;

namespace FluentTechnology.Domain.Interfaces;

public interface IOrganizationTypesRepository
{
    Task<IEnumerable<OrganizationTypes>> GetAllAsync();
    Task<OrganizationTypes?> GetByIdAsync(Guid id);
    Task<OrganizationTypes> AddAsync(OrganizationTypes organizationType);
    Task UpdateAsync(OrganizationTypes organizationType);
    Task DeleteAsync(Guid id);
}
