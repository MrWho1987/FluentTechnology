namespace FluentTechnology.Domain.Entities;

/// <summary>
/// Stores user information including their preferred communication method and organization type.
/// </summary>
public partial class Users
{
    public string EmailAddress { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public Guid PreferredCommunicationMethodId { get; set; }

    public Guid OrganizationTypeId { get; set; }

    public virtual OrganizationTypes OrganizationType { get; set; } = null!;

    public virtual PreferredCommunicationMethods PreferredCommunicationMethod { get; set; } = null!;

    public virtual ICollection<GrantCategories> GrantCategory { get; set; } = new List<GrantCategories>();

    public virtual ICollection<PersonalizedContentPreferences> Preference { get; set; } = new List<PersonalizedContentPreferences>();
}
