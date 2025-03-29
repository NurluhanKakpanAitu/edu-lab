using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Application.Auth.Dto;
using Application.Auth.Vm;
using Application.Interfaces;
using Application.Users.Vm;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Application.Auth;

using System.IdentityModel.Tokens.Jwt;

public class AuthService(
    IApplicationDbContext context,
    IHttpContextAccessor httpContextAccessor,
    IOptions<JwtSettings> jwtSettings,
    IMapper mapper
    ) : IAuthService
{
    
    private readonly JwtSettings _jwtSettings = jwtSettings.Value;
    
    public async Task<TokenVm> Login(string email, string password)
    {
        var user = await context.Users.FirstOrDefaultAsync(x => x.Email == email);
        if (user == null || !VerifyPassword(password, user.Password!))
            throw new Exception("emailOrPasswordNotValid");
        
        var refreshToken = Guid.NewGuid().ToString();
        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpirationInDays);
        
        await context.SaveChangesAsync();
        
        var accessToken = GenerateToken(user);
        return new TokenVm
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };
    }

    public async Task Logout()
    {
        var userId = httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new UnauthorizedAccessException("User not authorized.");
        
        var user = await context.Users.FindAsync(Guid.Parse(userId));
        
        if (user == null)
            throw new InvalidOperationException("User not found.");
        
        user.RefreshToken = null;
        user.RefreshTokenExpiry = null;
        
        httpContextAccessor.HttpContext!.Response.Cookies.Delete("refreshToken");
        
        await context.SaveChangesAsync();
    }

    public async Task<TokenVm> Register(string email, string password)
    {
        // Check if user already exists
        var existingUser = await context.Users.FirstOrDefaultAsync(x => x.Email == email);
        if (existingUser != null)
            throw new InvalidOperationException("Пользователь уже существует.");

        // Create user
        var user = new Domain.Entities.User
        {
            Id = Guid.NewGuid(),
            Email = email,
            Password = HashPassword(password),
            Nickname = email.Split('@')[0], 
            Role = Role.User,
            CreatedAt = DateTime.UtcNow
        };
        
        
        var refreshToken = Guid.NewGuid().ToString();
        
        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpirationInDays);
        
        
        await context.Users.AddAsync(user);
        await context.SaveChangesAsync();

        
        var accessToken = GenerateToken(user);
        
        return new TokenVm
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };
    }

    public async Task<string?> WithGoogle(string googleToken)
    {
        var googleEmail = VerifyGoogleToken(googleToken);
        
        var user = await GetUserForGoogle(googleEmail);
        
        var refreshToken = Guid.NewGuid().ToString();
        
        user.RefreshToken = refreshToken;
        
        user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpirationInDays);
        
        httpContextAccessor.HttpContext!.Response.Cookies.Append("refreshToken", refreshToken, new CookieOptions
        {
            HttpOnly = true,
            Expires = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpirationInDays)
        });
        
        await context.SaveChangesAsync();
        
        return GenerateToken(user);
    }

    public async Task<UserVm> GetInfo(CancellationToken cancellationToken = default)
    {
        var userId = httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        
        if (userId == null)
            throw new UnauthorizedAccessException("Пользователь не авторизован.");

        var user = await context.Users
            .ProjectTo<UserVm>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(x => x.Id == Guid.Parse(userId), cancellationToken);
        
        if (user == null)
            throw new InvalidOperationException("Пользователь не найден.");
        
        return user;
    }

    public async Task<TokenVm> RefreshToken(string refreshToken)
    {
        var user = await context.Users.FirstOrDefaultAsync(x => x.RefreshToken == refreshToken);
        
        if (user == null || user.RefreshTokenExpiry < DateTime.UtcNow)
            throw new UnauthorizedAccessException("Refresh token is invalid.");
        
        var newRefreshToken = Guid.NewGuid().ToString();
        
        user.RefreshToken = newRefreshToken;
        user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpirationInDays);
        
        await context.SaveChangesAsync();
        
        
        var accessToken = GenerateToken(user);
        
        return new TokenVm
        {
            AccessToken = accessToken,
            RefreshToken = newRefreshToken
        };
    }

    // Helper methods
    private static bool VerifyPassword(string password, string passwordHash)
    {
        return BCrypt.Net.BCrypt.Verify(password, passwordHash);
    }

    private static string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    private string GenerateToken(Domain.Entities.User user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Email, user.Email),
            new(ClaimTypes.Name, user.Nickname),
            new(ClaimTypes.Role, user.Role.ToString())
        };
        
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
        
        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpirationInMinutes),
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature),
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience
        );
        
        var tokenHandler = new JwtSecurityTokenHandler();
        
        return tokenHandler.WriteToken(token);
    }

    private string VerifyGoogleToken(string googleToken)
    {
        // Simulate verifying Google token and retrieving email
        return "googleuser@example.com"; // Replace with actual Google token verification
    }

    private async Task<Domain.Entities.User> GetUserForGoogle(string email)
    {
        // Check if user already exists
        var user = await context.Users.FirstOrDefaultAsync(x => x.Email == email);
        if (user == null)
        {
            user = new Domain.Entities.User
            {
                Email = email,
                Nickname = email.Split('@')[0],
                Role = Role.User,
                CreatedAt = DateTime.UtcNow
            };

            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();
        }

        return user;

    }
}