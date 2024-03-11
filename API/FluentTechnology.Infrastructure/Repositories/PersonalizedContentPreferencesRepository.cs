using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentTechnology.Domain.Entities;
using FluentTechnology.Domain.Interfaces;
using FluentTechnology.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FluentTechnology.Infrastructure.Repositories;

/// <summary>
/// Implements the repository interface for managing personalized content preferences.
/// </summary>
public class PersonalizedContentPreferencesRepository : IPersonalizedContentPreferencesRepository
{
    private readonly ApplicationDbContext _context;

    public PersonalizedContentPreferencesRepository(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<IEnumerable<PersonalizedContentPreferences>> GetAllAsync()
    {
        return await _context.PersonalizedContentPreferences.ToListAsync();
    }

    public async Task<PersonalizedContentPreferences?> GetByIdAsync(Guid id)
    {
        return await _context.PersonalizedContentPreferences
                             .FindAsync(id);
    }

    public async Task<PersonalizedContentPreferences> AddAsync(PersonalizedContentPreferences personalizedContentPreference)
    {
        if (personalizedContentPreference == null) throw new ArgumentNullException(nameof(personalizedContentPreference));
        _context.PersonalizedContentPreferences.Add(personalizedContentPreference);
        await _context.SaveChangesAsync();
        return personalizedContentPreference;
    }

    public async Task UpdateAsync(PersonalizedContentPreferences personalizedContentPreference)
    {
        if (personalizedContentPreference == null) throw new ArgumentNullException(nameof(personalizedContentPreference));
        _context.Entry(personalizedContentPreference).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var preference = await _context.PersonalizedContentPreferences.FindAsync(id);
        if (preference != null)
        {
            _context.PersonalizedContentPreferences.Remove(preference);
            await _context.SaveChangesAsync();
        }
    }
    public async Task<IEnumerable<PersonalizedContentPreferences>> GetManyByPreferenceIdsAsync(IEnumerable<Guid> ids)
    {
        return await _context.PersonalizedContentPreferences
                             .Where(pcp => ids.Contains(pcp.Id))
                             .ToListAsync();
    }
}
