namespace JoakDAXPWebApp.Helpers
{
    public class AppSettings
    {
        /// <summary>
        /// Secret used to generate JWT.
        /// </summary>
        public string Secret { get; set; }

        /// <summary>
        /// Number of hours to set the token expiration
        /// </summary>
        public int TokenExpirationHours { get; set; }
    }
}
