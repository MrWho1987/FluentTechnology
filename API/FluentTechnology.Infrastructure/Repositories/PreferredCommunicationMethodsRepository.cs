using FluentTechnology.Domain.Entities;
using FluentTechnology.Domain.Interfaces;
using FluentTechnology.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FluentTechnology.Infrastructure.Repositories;

/// <summary>
/// Implements the repository interface for managing preferred communication methods.
/// </summary>
public class PreferredCommunicationMethodsRepository : IPreferredCommunicationMethodsRepository
{
    private readonly ApplicationDbContext _context;

    public PreferredCommunicationMethodsRepository(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<IEnumerable<PreferredCommunicationMethods>> GetAllAsync()
    {
        return await _context.PreferredCommunicationMethods.ToListAsync();
    }

    public async Task<PreferredCommunicationMethods?> GetByIdAsync(Guid id)
    {
        return await _context.PreferredCommunicationMethods
                             .FindAsync(id);
    }

    public async Task<PreferredCommunicationMethods> AddAsync(PreferredCommunicationMethods preferredCommunicationMethod)
    {
        if (preferredCommunicationMethod == null) throw new ArgumentNullException(nameof(preferredCommunicationMethod));
        _context.PreferredCommunicationMethods.Add(preferredCommunicationMethod);
        await _context.SaveChangesAsync();
        return preferredCommunicationMethod;
    }

    public async Task UpdateAsync(PreferredCommunicationMethods preferredCommunicationMethod)
    {
        if (preferredCommunicationMethod == null) throw new ArgumentNullException(nameof(preferredCommunicationMethod));
        _context.Entry(preferredCommunicationMethod).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var method = await _context.PreferredCommunicationMethods.FindAsync(id);
        if (method != null)
        {
            _context.PreferredCommunicationMethods.Remove(method);
            await _context.SaveChangesAsync();
        }
    }
}
