using a.Dto;
using NET.Dto;

namespace StoreApi.Interfaces;

public interface IUserService
{
    Task<IEnumerable<UserDto>> GetAllUsersAsync();
    Task<UserDto?> GetUserByIdAsync(int id);
    Task<UserDto> CreateUserAsync(CreateUserDto createDto);
    Task<UserDto?> UpdateUserAsync(int id, CreateUserDto updateDto);
    Task<bool> DeleteUserAsync(int id);
    Task<LoginResponeDto?> AuthenticateAsync(string email, string password);
}