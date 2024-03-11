using FluentTechnology.Domain.Entities;

namespace FluentTechnology.Domain.Interfaces;

public interface IGrantCategoriesRepository
{
    Task<IEnumerable<GrantCategories>> GetAllAsync();
    Task<GrantCategories?> GetByIdAsync(Guid id);
    Task<GrantCategories> AddAsync(GrantCategories grantCategory);
    Task UpdateAsync(GrantCategories grantCategory);
    Task DeleteAsync(Guid id);
    Task<IEnumerable<GrantCategories>> GetManyByCategoryIdsAsync(IEnumerable<Guid> ids);
}
