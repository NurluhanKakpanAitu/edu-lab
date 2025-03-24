using Application.Users.Dto;
using Domain.Entities;

namespace Application.Users.Services;

public interface IUserService
{
    Task UpdateUserAsync(Guid id, UserDto request, CancellationToken cancellationToken = default);
    Task<UserTestResult?> GetUserTestResultAsync(Guid testId, CancellationToken cancellationToken);
}