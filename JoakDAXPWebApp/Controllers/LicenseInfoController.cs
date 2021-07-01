using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JoakDAXPWebApp.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JoakDAXPWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsPolicy")]
    public class LicenseInfoController : ControllerBase
    {
        #region PROPERTIES

        private ILicenseInfoService _licenseInfoService;

        #endregion

        #region METHODS

        public LicenseInfoController(ILicenseInfoService licenseInfoService)
        {
            this._licenseInfoService = licenseInfoService;
        }

        public IActionResult GetAll()
        {
            return Ok(_licenseInfoService.GetAll());
        }

        #endregion
    }
}
