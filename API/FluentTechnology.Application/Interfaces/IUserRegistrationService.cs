using FluentTechnology.Application.DTOs;

namespace FluentTechnology.Application.Interfaces
{
    public interface IUserRegistrationService
    {
        Task RegisterUserAsync(UserRegistrationDto userDto);
    }
}
