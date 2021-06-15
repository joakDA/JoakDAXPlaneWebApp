using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Threading.Tasks;
using JoakDAXPWebApp.Helpers;
using JoakDAXPWebApp.Services;
using JoakDAXPWebApp.Interfaces;

namespace JoakDAXPWebApp.Authorization
{
    /// <summary>
    /// Class used to extract the JWT token from the request Authorization header and validates it with the
    /// jwtUtils.ValidateToken() method.
    /// </summary>
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AppSettings _appSettings;

        public JwtMiddleware(RequestDelegate next, IOptions<AppSettings> appSettings)
        {
            _next = next;
            _appSettings = appSettings.Value;
        }

        public async Task Invoke(HttpContext context, IUserService userService, IJwtUtils jwtUtils)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var userId = jwtUtils.ValidateToken(token);
            if (userId != null)
            {
                // attach user to context on successful jwt validation
                context.Items["User"] = userService.GetById(userId.Value);
            }

            await _next(context);
        }
    }
}
