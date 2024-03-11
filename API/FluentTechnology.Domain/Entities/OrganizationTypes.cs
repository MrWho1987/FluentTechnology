namespace FluentTechnology.Domain.Entities;

/// <summary>
/// Stores types of organizations (e.g., non-profit, educational institution).
/// </summary>
public partial class OrganizationTypes
{
    public Guid Id { get; set; }

    public string TypeName { get; set; } = null!;

    public virtual ICollection<Users> Users { get; set; } = new List<Users>();
}
