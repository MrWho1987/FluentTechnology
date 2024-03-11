using FluentTechnology.Domain.Entities;

namespace FluentTechnology.Domain.Interfaces;

public interface IUsersRepository
{
    Task<IEnumerable<Users>> GetAllAsync();
    Task<Users?> GetByIdAsync(string emailAddress);
    Task<Users> AddAsync(Users user);
    Task UpdateAsync(Users user);
    Task DeleteAsync(string emailAddress);
}
