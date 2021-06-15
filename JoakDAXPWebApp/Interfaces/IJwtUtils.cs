using JoakDAXPWebApp.Entities;

namespace JoakDAXPWebApp.Interfaces
{
    public interface IJwtUtils
    {
        /// <summary>
        /// Generate a token for the user.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public string GenerateToken(User user);

        /// <summary>
        /// Validate is token is valid.
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public int? ValidateToken(string token);
    }
}
