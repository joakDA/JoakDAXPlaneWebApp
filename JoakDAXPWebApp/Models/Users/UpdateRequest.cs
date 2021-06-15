namespace JoakDAXPWebApp.Models.Users
{
    /// <summary>
	/// Model used to update an user data. Is the same as register model but without required attribute
    /// so the fields not posted will not be updated.
	/// </summary>
    public class UpdateRequest
    {
        public string Username { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Password { get; set; }
    }
}
