using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using JoakDAXPWebApp.Authorization;
using JoakDAXPWebApp.Helpers;
using JoakDAXPWebApp.Models;
using JoakDAXPWebApp.Services;
using JoakDAXPWebApp.Interfaces;
using JoakDAXPWebApp.Models.Users;
using Microsoft.AspNetCore.Cors;
using System.Collections.Generic;
using JoakDAXPWebApp.Entities;
using JoakDAXPWebApp.Models.DataTable;
using Newtonsoft.Json;

namespace JoakDAXPWebApp.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    [EnableCors("CorsPolicy")]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        public UsersController(
            IUserService userService,
            IMapper mapper,
            IOptions<AppSettings> appSettings)
        {
            _userService = userService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate(AuthenticateRequest model)
        {
            var response = _userService.Authenticate(model);
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register(RegisterRequest model)
        {
            _userService.Register(model);
            return Ok(new { message = "Registration successful" });
        }

        [HttpPost]
        public IActionResult GetAll(DataTableRequestModel model)
        {
            //DataTableRequestModel model = JsonConvert.DeserializeObject<DataTableRequestModel>(jsonData);
            int recordsTotal = 0;
            int recordsFiltered = 0;
            IList<User> users = (IList<User>)_userService.GetAll(model, out recordsTotal, out recordsFiltered);

            DataTableResponseModel<User> usersResponse = new DataTableResponseModel<User>(users, model.draw, recordsFiltered, recordsTotal);

            return Ok(usersResponse);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var user = _userService.GetById(id);
            return Ok(user);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, UpdateRequest model)
        {
            _userService.Update(id, model);
            return Ok(new { message = "User updated successfully" });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _userService.Delete(id);
            return Ok(new { message = "User deleted successfully" });
        }
    }
}
