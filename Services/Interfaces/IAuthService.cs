using PROG7311_POE.Models;

namespace PROG7311_POE.Services.Interfaces
{
    public interface IAuthService
    {
        //°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°//

        // Validates a user's credentials by checking if the provided username and password are correct.
        Task<bool> ValidateUserAsync(string username, string password);

        // Retrieves a user object based on the provided username
        Task<User> GetUserByUsernameAsync(string username);

        // Generates a securely hashed password using a random salt 
        string HashPassword(string password);

        // Verifies whether the provided password matches the stored hashed password
        bool VerifyPassword(string hashedPassword, string providedPassword);
    
        // Signs in the user by issuing an authentication cookie and setting user claims
        Task SignInUserAsync(HttpContext httpContext, User user, bool isPersistent);

        // Signs out the currently logged-in user and clears the authentication cookie
        Task SignOutUserAsync(HttpContext httpContext);

        // Registers a new user account with a username, password, and role
        Task<User> RegisterUserAsync(string username, string password, string role);

        //°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°//
    }
}//°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°...ooo000 END OF FILE 000ooo...°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°//
