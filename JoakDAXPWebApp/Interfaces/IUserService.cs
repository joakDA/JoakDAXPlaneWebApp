using System.Collections.Generic;
using JoakDAXPWebApp.Entities;
using JoakDAXPWebApp.Models.DataTable;
using JoakDAXPWebApp.Models.Users;

namespace JoakDAXPWebApp.Interfaces
{
    /// <summary>
    /// Methods for managing users.
    /// </summary>
    public interface IUserService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);
        IList<User> GetAll(DataTableRequestModel model, out int recordsTotal, out int recordsFiltered);
        User GetById(int id);
        void Register(RegisterRequest model);
        void Update(int id, UpdateRequest model);
        void Delete(int id);
    }
}
