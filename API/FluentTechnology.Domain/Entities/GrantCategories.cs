namespace FluentTechnology.Domain.Entities;

/// <summary>
/// Stores grant categories of interest (e.g., Education, Health).
/// </summary>
public partial class GrantCategories
{
    public Guid Id { get; set; }

    public string CategoryName { get; set; } = null!;

    public virtual ICollection<Users> EmailAddress { get; set; } = new List<Users>();
}
