using System;
namespace JoakDAXPWebApp.Entities
{
    public class BaseEntity
    {
        /// <summary>
        /// When user was created.
        /// </summary>
        public DateTime CreatedDate { get; set; }
        /// <summary>
        /// When user was updated.
        /// </summary>
        public DateTime UpdatedDate { get; set; }
    }
}
