using Application.Auth.Vm;
using Application.Users.Vm;

namespace Application.Auth;

public interface IAuthService
{
    /// <summary>
    /// Logs in a user with email and password.
    /// </summary>
    /// <param name="email">The email address of the user.</param>
    /// <param name="password">The password of the user.</param>
    /// <returns>Returns the authentication token or user information.</returns>
    Task<TokenVm> Login(string email, string password);

    /// <summary>
    /// Logs out the current user.
    /// </summary>
    /// <returns>Returns a Task to indicate completion.</returns>
    Task Logout();

    /// <summary>
    /// Registers a new user with email and password.
    /// </summary>
    /// <param name="email">The email address of the new user.</param>
    /// <param name="password">The password for the new user.</param>
    /// <returns>Returns the user ID or status of the operation.</returns>
    Task<TokenVm> Register(string email, string password);
    
    Task<UserVm> GetInfo(CancellationToken cancellationToken = default);
    
    
    Task<TokenVm> RefreshToken(string refreshToken);
}