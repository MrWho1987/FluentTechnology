namespace FluentTechnology.Domain.Entities;

/// <summary>
/// Stores personalized content preferences (e.g., News and Updates, Success Stories).
/// </summary>
public partial class PersonalizedContentPreferences
{
    public Guid Id { get; set; }

    public string PreferenceName { get; set; } = null!;

    public virtual ICollection<Users> EmailAddress { get; set; } = new List<Users>();
}
