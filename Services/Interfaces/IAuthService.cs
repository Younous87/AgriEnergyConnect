using PROG7311_POE.Models;

namespace PROG7311_POE.Services.Interfaces
{
    public interface IAuthService
    {
        Task<bool> ValidateUserAsync(string username, string password);
        Task<User> GetUserByUsernameAsync(string username);
        string HashPassword(string password);
        bool VerifyPassword(string hashedPassword, string providedPassword);
        Task SignInUserAsync(HttpContext httpContext, User user, bool isPersistent);
        Task SignOutUserAsync(HttpContext httpContext);
        Task<User> RegisterUserAsync(string username, string password, string role);
    }
}
