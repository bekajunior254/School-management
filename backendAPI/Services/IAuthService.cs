using Microsoft.AspNetCore.Identity;
using School_Management_System.DTOs;

namespace School_Management_System.Services
{
    public interface IAuthService
    {
        /// <summary>
        /// Registers a new user with the specified credentials and role.
        /// </summary>
        /// <param name="model">The registration data including username, email, password, and role.</param>
        /// <returns>The result of the identity creation process.</returns>
        Task<IdentityResult> RegisterAsync(RegisterDto model);

        /// <summary>
        /// Authenticates a user and returns a JWT if successful.
        /// </summary>
        /// <param name="model">The login credentials.</param>
        /// <returns>A JWT token string.</returns>
        Task<string> LoginAsync(LoginDto model);
    }
}
