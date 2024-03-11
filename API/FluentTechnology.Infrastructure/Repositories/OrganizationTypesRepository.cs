using FluentTechnology.Domain.Entities;
using FluentTechnology.Domain.Interfaces;
using FluentTechnology.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FluentTechnology.Infrastructure.Repositories;

/// <summary>
/// Implements the repository interface for managing organization types.
/// </summary>
public class OrganizationTypesRepository : IOrganizationTypesRepository
{
    private readonly ApplicationDbContext _context;

    public OrganizationTypesRepository(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<IEnumerable<OrganizationTypes>> GetAllAsync()
    {
        return await _context.OrganizationTypes.ToListAsync();
    }

    public async Task<OrganizationTypes?> GetByIdAsync(Guid id)
    {
        return await _context.OrganizationTypes.FindAsync(id);
    }

    public async Task<OrganizationTypes> AddAsync(OrganizationTypes organizationType)
    {
        if (organizationType == null) throw new ArgumentNullException(nameof(organizationType));
        _context.OrganizationTypes.Add(organizationType);
        await _context.SaveChangesAsync();
        return organizationType;
    }

    public async Task UpdateAsync(OrganizationTypes organizationType)
    {
        if (organizationType == null) throw new ArgumentNullException(nameof(organizationType));
        _context.Entry(organizationType).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var organizationType = await _context.OrganizationTypes.FindAsync(id);
        if (organizationType != null)
        {
            _context.OrganizationTypes.Remove(organizationType);
            await _context.SaveChangesAsync();
        }
    }
}
