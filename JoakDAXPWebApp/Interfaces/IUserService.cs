using System.Collections.Generic;
using JoakDAXPWebApp.Entities;
using JoakDAXPWebApp.Models.Users;

namespace JoakDAXPWebApp.Interfaces
{
    /// <summary>
    /// Methods for managing users.
    /// </summary>
    public interface IUserService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);
        IEnumerable<User> GetAll();
        User GetById(int id);
        void Register(RegisterRequest model);
        void Update(int id, UpdateRequest model);
        void Delete(int id);
    }
}
