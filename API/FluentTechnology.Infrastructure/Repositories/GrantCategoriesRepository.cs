using FluentTechnology.Domain.Entities;
using FluentTechnology.Domain.Interfaces;
using FluentTechnology.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FluentTechnology.Infrastructure.Repositories;

/// <summary>
/// Implements the repository interface for managing grant categories.
/// </summary>
public class GrantCategoriesRepository : IGrantCategoriesRepository
{
    private readonly ApplicationDbContext _context;

    public GrantCategoriesRepository(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<IEnumerable<GrantCategories>> GetAllAsync()
    {
        return await _context.GrantCategories.ToListAsync();
    }

    public async Task<GrantCategories?> GetByIdAsync(Guid id)
    {
        return await _context.GrantCategories.FindAsync(id);
    }

    public async Task<GrantCategories> AddAsync(GrantCategories grantCategory)
    {
        if (grantCategory == null) throw new ArgumentNullException(nameof(grantCategory));
        _context.GrantCategories.Add(grantCategory);
        await _context.SaveChangesAsync();
        return grantCategory;
    }

    public async Task UpdateAsync(GrantCategories grantCategory)
    {
        if (grantCategory == null) throw new ArgumentNullException(nameof(grantCategory));
        _context.Entry(grantCategory).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var grantCategory = await _context.GrantCategories.FindAsync(id);
        if (grantCategory != null)
        {
            _context.GrantCategories.Remove(grantCategory);
            await _context.SaveChangesAsync();
        }
    }
    public async Task<IEnumerable<GrantCategories>> GetManyByCategoryIdsAsync(IEnumerable<Guid> ids)
    {
        return await _context.GrantCategories
                             .Where(gc => ids.Contains(gc.Id))
                             .ToListAsync();
    }
}
