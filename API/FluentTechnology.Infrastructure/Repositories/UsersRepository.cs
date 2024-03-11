using FluentTechnology.Domain.Entities;
using FluentTechnology.Domain.Interfaces;
using FluentTechnology.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FluentTechnology.Infrastructure.Repositories;

/// <summary>
/// Implements the repository interface for user-related data operations.
/// </summary>
public class UsersRepository : IUsersRepository
{
    private readonly ApplicationDbContext _context;

    public UsersRepository(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<IEnumerable<Users>> GetAllAsync()
    {
        return await _context.Users
                             .Include(u => u.OrganizationType)
                             .Include(u => u.PreferredCommunicationMethod)
                             .Include(u => u.GrantCategory)
                             .Include(u => u.Preference)
                             .ToListAsync();
    }

    public async Task<Users?> GetByIdAsync(string emailAddress)
    {
        return await _context.Users
                             .Include(u => u.OrganizationType)
                             .Include(u => u.PreferredCommunicationMethod)
                             .Include(u => u.GrantCategory)
                             .Include(u => u.Preference)
                             .FirstOrDefaultAsync(u => u.EmailAddress == emailAddress);
    }

    public async Task<Users> AddAsync(Users user)
    {
        if (user == null) throw new ArgumentNullException(nameof(user));
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task UpdateAsync(Users user)
    {
        if (user == null) throw new ArgumentNullException(nameof(user));
        _context.Entry(user).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(string emailAddress)
    {
        var user = await _context.Users.FindAsync(emailAddress);
        if (user != null)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}
