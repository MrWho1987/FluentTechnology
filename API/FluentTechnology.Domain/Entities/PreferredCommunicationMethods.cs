namespace FluentTechnology.Domain.Entities;

/// <summary>
/// Stores preferred communication methods (e.g., email, SMS).
/// </summary>
public partial class PreferredCommunicationMethods
{
    public Guid Id { get; set; }

    public string Method { get; set; } = null!;

    public virtual ICollection<Users> Users { get; set; } = new List<Users>();
}
