using System.Security.Claims;
using Application.Interfaces;
using Application.Users.Dto;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Application.Users.Services;

public class UserService(
    IApplicationDbContext dbContext,
    IHttpContextAccessor contextAccessor
    ) : IUserService
{
    public async Task UpdateUserAsync(Guid id, UserDto request, CancellationToken cancellationToken = default)
    {
        var user = await dbContext.Users.FindAsync([id], cancellationToken);
        
        if (user is null)
            throw new Exception("Пользователь не найден");
        
        
        user.Nickname = request.Nickname;
        user.Email = request.Email;
        user.About = request.About;
        user.PhotoPath = request.PhotoPath;

        if (!string.IsNullOrWhiteSpace(request.Password))
        {
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
            user.Password = passwordHash;
        }
        
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<UserTestResult?> GetUserTestResultAsync(Guid testId, CancellationToken cancellationToken)
    {
        var userId = contextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        
        if (userId is null)
            throw new UnauthorizedAccessException("Пользователь не авторизован");
        
        var userTestResult = await dbContext.UserTestResults
            .FirstOrDefaultAsync(x => x.UserId == Guid.Parse(userId) && x.TestId == testId, cancellationToken);

        return userTestResult;
    }
}