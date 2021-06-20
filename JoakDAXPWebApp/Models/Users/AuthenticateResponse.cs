namespace JoakDAXPWebApp.Models.Users
{
    /// <summary>
    /// Class used as response when an user is successfully authenticated on the webapp.
    /// </summary>
    public class AuthenticateResponse
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Token { get; set; }
    }
}
