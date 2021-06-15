using AutoMapper;
using JoakDAXPWebApp.Entities;
using JoakDAXPWebApp.Models;
using JoakDAXPWebApp.Models.Users;

namespace JoakDAXPWebApp.Helpers
{
    /// <summary>
    /// Map User entities with other classes such as <c>AuthenticateResponse</c>, <c>RegisterRequest</c>
    /// and <c>UpdateRequest</c>
    /// </summary>
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // User -> AuthenticateResponse
            CreateMap<User, AuthenticateResponse>();

            // RegisterRequest -> User
            CreateMap<RegisterRequest, User>();

            // UpdateRequest -> User
            CreateMap<UpdateRequest, User>()
                .ForAllMembers(x => x.Condition(
                    (src, dest, prop) =>
                    {
                        // ignore null & empty string properties
                        if (prop == null) return false;
                        if (prop.GetType() == typeof(string) && string.IsNullOrEmpty((string)prop)) return false;

                        return true;
                    }
                ));
        }
    }
}
