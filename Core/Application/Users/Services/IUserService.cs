using Application.Users.Dto;

namespace Application.Users.Services;

public interface IUserService
{
    Task UpdateUserAsync(Guid id, UserDto request, CancellationToken cancellationToken = default);
}