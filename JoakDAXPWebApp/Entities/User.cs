using System.Text.Json.Serialization;

namespace JoakDAXPWebApp.Entities
{
    public class User : BaseEntity
    {
        /// <summary>
        /// Unique identifier of user stored on database.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Username used by the user to log in to access the functionalities.
        /// </summary>
        public string Username { get; set; }
        /// <summary>
        /// Email address asociated to the user. Useful for resseting password for example.
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Name of the user.
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// Lastname of the user.
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// Password encrypted for the user.
        /// </summary>
        [JsonIgnore]
        public string Password { get; set; }
    }
}
