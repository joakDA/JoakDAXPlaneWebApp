using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Reflection;
using AutoMapper;
using JoakDAXPWebApp.Data;
using JoakDAXPWebApp.Entities;
using JoakDAXPWebApp.Exceptions;
using JoakDAXPWebApp.Interfaces;
using JoakDAXPWebApp.Models.DataTable;
using JoakDAXPWebApp.Models.Users;
using XPlaneUDPExchange.Helpers;
using BCryptNet = BCrypt.Net.BCrypt;

namespace JoakDAXPWebApp.Services
{
    public class UserService : IUserService, IDatabaseService
    {
        private ApplicationDbContext _context;
        private IJwtUtils _jwtUtils;
        private readonly IMapper _mapper;

        public UserService(
            ApplicationDbContext context,
            IJwtUtils jwtUtils,
            IMapper mapper)
        {
            _context = context;
            _jwtUtils = jwtUtils;
            _mapper = mapper;
        }

        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
            var user = _context.Users.SingleOrDefault(x => x.Username == model.Username);

            // validate
            if (user == null || !BCryptNet.Verify(model.Password, user.Password))
                throw new AppException("Username or password is incorrect");

            // authentication successful
            var response = _mapper.Map<AuthenticateResponse>(user);
            response.Token = _jwtUtils.GenerateToken(user);
            return response;
        }

        public IList<User> GetAll(DataTableRequestModel model, out int recordsTotal, out int recordsFiltered)
        {
            int draw = 0;
            int start = 0;
            int length = 0;
            recordsTotal = 0;
            recordsFiltered = 0;
            IQueryable<User> users;
            try
            {
                string search = model.search != null ? model.search.value.ToUpper() : "";
                draw = model.draw;
                // Find paging info
                start = model.start;
                length = model.length;
                // Sort
                string sortColumn = ConvertColumnIndexToName(model.order.FirstOrDefault().column);
                string sortColumnDir = model.order.FirstOrDefault().dir;

                users = _context.Users.AsQueryable();

                recordsTotal = users.Count();

                users = users.Where(x => x.FirstName.ToUpper().Contains(search) ||
                x.LastName.ToUpper().Contains(search) || x.Username.ToUpper().Contains(search) ||
                x.Email.ToUpper().Contains(search));

                if (!string.IsNullOrEmpty(sortColumn) && !string.IsNullOrEmpty(sortColumnDir)) { 
                    users = users.OrderBy(sortColumn + " " + sortColumnDir);
                }

                recordsFiltered = users.Count();

                users = users.Skip((start)).Take(length);

                return users.ToList();
            }catch(Exception exception1)
            {
                LogHelper.Func_WriteEventInLogFile(DateTime.Now.ToLocalTime(), Enum_EventTypes.Error, "", string.Format("{0}.{1}()", MethodBase.GetCurrentMethod().DeclaringType.FullName, MethodBase.GetCurrentMethod().Name), "GeneralException",
                  "Source = " + exception1.Source.Replace("'", "''") + ", Message = " + exception1.Message.Replace("'", "''"));
                recordsFiltered = 0;
                recordsTotal = 0;
                return new List<User>();
            }
        }

        /// <summary>
        /// Convert the numeric index of the column to the column name to make sort possible on the
        /// datable server side.
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        public string ConvertColumnIndexToName(int column)
        {
            string columnName = string.Empty;
            try
            {
                switch (column)
                {
                    case 0:
                        columnName = "Id";
                        break;
                    case 1:
                        columnName = "FirstName";
                        break;
                    case 2:
                        columnName = "LastName";
                        break;
                    case 3:
                        columnName = "Email";
                        break;
                    case 4:
                        columnName = "Username";
                        break;
                    default:
                        break;
                }
            }
            catch (Exception exception1)
            {
                LogHelper.Func_WriteEventInLogFile(DateTime.Now.ToLocalTime(), Enum_EventTypes.Error, "", string.Format("{0}.{1}()", MethodBase.GetCurrentMethod().DeclaringType.FullName, MethodBase.GetCurrentMethod().Name), "GeneralException",
                  "Source = " + exception1.Source.Replace("'", "''") + ", Message = " + exception1.Message.Replace("'", "''"));
            }
            return columnName;
        }

        public User GetById(int id)
        {
            return getUser(id);
        }

        public void Register(RegisterRequest model)
        {
            // validate
            if (_context.Users.Any(x => x.Username == model.Username))
                throw new AppException("Username '" + model.Username + "' is already taken");

            // map model to new user object
            var user = _mapper.Map<User>(model);

            // hash password
            user.Password = BCryptNet.HashPassword(model.Password);

            // save user
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void Update(int id, UpdateRequest model)
        {
            var user = getUser(id);

            // validate
            if (model.Username != user.Username && _context.Users.Any(x => x.Username == model.Username))
                throw new AppException("Username '" + model.Username + "' is already taken");

            // hash password if it was entered
            if (!string.IsNullOrEmpty(model.Password))
                model.Password = BCryptNet.HashPassword(model.Password);

            // copy model to user and save
            _mapper.Map(model, user);
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var user = getUser(id);
            _context.Users.Remove(user);
            _context.SaveChanges();
        }

        // helper methods

        private User getUser(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null) throw new KeyNotFoundException("User not found");
            return user;
        }
    }
}
