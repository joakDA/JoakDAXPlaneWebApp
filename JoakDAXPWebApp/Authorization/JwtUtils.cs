using System;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using JoakDAXPWebApp.Helpers;
using JoakDAXPWebApp.Interfaces;
using JoakDAXPWebApp.Entities;

namespace JoakDAXPWebApp.Authorization
{
    public class JwtUtils : IJwtUtils
    {
        private readonly AppSettings _appSettings;

        /// <summary>
        /// Use DI to inject app settings used to retrieve the secret.
        /// </summary>
        /// <param name="appSettings"></param>
        public JwtUtils(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        /// <summary>
        /// Generate the token for the specified user.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public string GenerateToken(User user)
        {
            // generate token that is valid for 7 days
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            int tokenExpirationHours = _appSettings.TokenExpirationHours;
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddHours(tokenExpirationHours),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public int? ValidateToken(string token)
        {
            if (token == null)
                return null;

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                JwtSecurityToken jwtToken = (JwtSecurityToken)validatedToken;
                int userId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);

                // return user id from JWT token if validation successful
                return userId;
            }
            catch
            {
                // return null if validation fails
                return null;
            }
        }
    }
}
