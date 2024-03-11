using System.ComponentModel.DataAnnotations;

namespace FluentTechnology.Application.DTOs
{
    public class UserRegistrationDto
    {
        [Required, MaxLength(255)]
        public string FullName { get; set; }

        [Required, EmailAddress]
        public string EmailAddress { get; set; }

        public Guid? PreferredCommunicationMethodId { get; set; }
        public Guid? OrganizationTypeId { get; set; }

        [MaxLength(255)]
        public string? CustomPreferredCommunicationMethod { get; set; }

        [MaxLength(255)]
        public string? CustomOrganizationTypeName { get; set; }

        public List<Guid> GrantCategoryIds { get; set; } = new List<Guid>();
        public List<Guid> PersonalizedContentPreferenceIds { get; set; } = new List<Guid>();
    }
}
