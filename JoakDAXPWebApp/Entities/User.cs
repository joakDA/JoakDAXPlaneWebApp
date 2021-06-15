using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace JoakDAXPWebApp.Entities
{
    [Index(nameof(Username), IsUnique = true)]
    [Index(nameof(Email))]
    [Index(nameof(FirstName))]
    [Index(nameof(LastName))]
    public class User : BaseEntity
    {
        /// <summary>
        /// Unique identifier of user stored on database.
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// Username used by the user to log in to access the functionalities.
        /// </summary>
        [MaxLength(50)]
        public string Username { get; set; }
        /// <summary>
        /// Email address asociated to the user. Useful for resseting password for example.
        /// </summary>
        [MaxLength(255)]
        public string Email { get; set; }
        /// <summary>
        /// Name of the user.
        /// </summary>
        [MaxLength(50)]
        public string FirstName { get; set; }
        /// <summary>
        /// Lastname of the user.
        /// </summary>
        [MaxLength(100)]
        public string LastName { get; set; }
        /// <summary>
        /// Password encrypted for the user.
        /// </summary>
        [JsonIgnore]
        public string Password { get; set; }
    }
}
