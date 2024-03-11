using FluentTechnology.Application.DTOs;
using FluentTechnology.Application.Interfaces;
using FluentTechnology.Domain.Entities;
using FluentTechnology.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace FluentTechnology.Application.Services
{
    /// <summary>
    /// Service responsible for handling user registration, including validation of input data
    /// and enforcement of business rules.
    /// </summary>
    public class UserRegistrationService : IUserRegistrationService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IPreferredCommunicationMethodsRepository _communicationMethodsRepository;
        private readonly IOrganizationTypesRepository _organizationTypesRepository;
        private readonly IGrantCategoriesRepository _grantCategoriesRepository;
        private readonly IPersonalizedContentPreferencesRepository _contentPreferencesRepository;

        public UserRegistrationService(
            IUsersRepository usersRepository,
            IPreferredCommunicationMethodsRepository communicationMethodsRepository,
            IOrganizationTypesRepository organizationTypesRepository,
            IGrantCategoriesRepository grantCategoriesRepository,
            IPersonalizedContentPreferencesRepository contentPreferencesRepository)
        {
            _usersRepository = usersRepository;
            _communicationMethodsRepository = communicationMethodsRepository;
            _organizationTypesRepository = organizationTypesRepository;
            _grantCategoriesRepository = grantCategoriesRepository;
            _contentPreferencesRepository = contentPreferencesRepository;
        }

        public async Task RegisterUserAsync(UserRegistrationDto userDto)
        {
            ValidateDto(userDto);

            var existingUser = await _usersRepository.GetByIdAsync(userDto.EmailAddress);
            if (existingUser != null)
            {
                throw new InvalidOperationException("A user with this email address already exists.");
            }

            var preferredMethodId = await HandleCustomPreferredCommunicationMethodAsync(userDto);
            var organizationTypeId = await HandleCustomOrganizationTypeAsync(userDto);

            var user = new Users
            {
                EmailAddress = userDto.EmailAddress,
                FullName = userDto.FullName,
                PreferredCommunicationMethodId = preferredMethodId.GetValueOrDefault(),
                OrganizationTypeId = organizationTypeId.GetValueOrDefault(),
                GrantCategory = new List<GrantCategories>(),
                Preference = new List<PersonalizedContentPreferences>()
            };

            // Corrected handling for GrantCategories and PersonalizedContentPreferences using loops
            var grantCategories = await FetchGrantCategories(userDto.GrantCategoryIds);
            foreach (var category in grantCategories)
            {
                user.GrantCategory.Add(category);
            }

            var contentPreferences = await FetchContentPreferences(userDto.PersonalizedContentPreferenceIds);
            foreach (var preference in contentPreferences)
            {
                user.Preference.Add(preference);
            }

            await _usersRepository.AddAsync(user);
        }

        private async Task<Guid?> HandleCustomPreferredCommunicationMethodAsync(UserRegistrationDto userDto)
        {
            if (!userDto.PreferredCommunicationMethodId.HasValue && !string.IsNullOrWhiteSpace(userDto.CustomPreferredCommunicationMethod))
            {
                var newMethod = new PreferredCommunicationMethods { Method = userDto.CustomPreferredCommunicationMethod };
                var addedMethod = await _communicationMethodsRepository.AddAsync(newMethod);
                return addedMethod.Id;
            }
            return userDto.PreferredCommunicationMethodId;
        }

        private async Task<Guid?> HandleCustomOrganizationTypeAsync(UserRegistrationDto userDto)
        {
            if (!userDto.OrganizationTypeId.HasValue && !string.IsNullOrWhiteSpace(userDto.CustomOrganizationTypeName))
            {
                var newType = new OrganizationTypes { TypeName = userDto.CustomOrganizationTypeName };
                var addedType = await _organizationTypesRepository.AddAsync(newType);
                return addedType.Id;
            }
            return userDto.OrganizationTypeId;
        }

        private void ValidateDto(UserRegistrationDto userDto)
        {
            var validationContext = new ValidationContext(userDto);
            Validator.ValidateObject(userDto, validationContext, validateAllProperties: true);
        }

        private async Task<IEnumerable<GrantCategories>> FetchGrantCategories(List<Guid> grantCategoryIds)
        {
            return await _grantCategoriesRepository.GetManyByCategoryIdsAsync(grantCategoryIds);
        }

        private async Task<IEnumerable<PersonalizedContentPreferences>> FetchContentPreferences(List<Guid> preferenceIds)
        {
            return await _contentPreferencesRepository.GetManyByPreferenceIdsAsync(preferenceIds);
        }
    }
}
