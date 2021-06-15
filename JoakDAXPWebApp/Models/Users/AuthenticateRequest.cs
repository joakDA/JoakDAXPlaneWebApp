using System.ComponentModel.DataAnnotations;
namespace JoakDAXPWebApp.Models.Users
{
    /// <summary>
    /// Model with data used to authenticate an user on the application.
    /// </summary>
    public class AuthenticateRequest
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
