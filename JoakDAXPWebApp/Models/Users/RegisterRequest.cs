using System.ComponentModel.DataAnnotations;
namespace JoakDAXPWebApp.Models.Users
{
    /// <summary>
    /// Model used to register an user on the webapp.
    /// </summary>
    public class RegisterRequest
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
