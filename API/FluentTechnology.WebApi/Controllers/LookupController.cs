using FluentTechnology.Application.DTOs;
using FluentTechnology.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class LookupController : ControllerBase
{
    private readonly ILookupService _lookupService;

    public LookupController(ILookupService lookupService)
    {
        _lookupService = lookupService;
    }

    [HttpGet("preferred-communication-methods")]
    public async Task<ActionResult<IEnumerable<LookupDto>>> GetPreferredCommunicationMethods()
    {
        var methods = await _lookupService.GetPreferredCommunicationMethodsAsync();
        return Ok(methods);
    }

    [HttpGet("organization-types")]
    public async Task<ActionResult<IEnumerable<LookupDto>>> GetOrganizationTypes()
    {
        var types = await _lookupService.GetOrganizationTypesAsync();
        return Ok(types);
    }

    [HttpGet("grant-categories")]
    public async Task<ActionResult<IEnumerable<LookupDto>>> GetGrantCategories()
    {
        var categories = await _lookupService.GetGrantCategoriesAsync();
        return Ok(categories);
    }

    [HttpGet("personalized-content-preferences")]
    public async Task<ActionResult<IEnumerable<LookupDto>>> GetPersonalizedContentPreferences()
    {
        var preferences = await _lookupService.GetPersonalizedContentPreferencesAsync();
        return Ok(preferences);
    }
}
