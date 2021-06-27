using JoakDAXPWebApp.Authorization;
using JoakDAXPWebApp.Entities;
using JoakDAXPWebApp.Interfaces;
using JoakDAXPWebApp.Models.DataTable;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JoakDAXPWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [EnableCors("CorsPolicy")]
    public class FlightsController : ControllerBase
    {
        #region PROPERTIES

        private IFlightService _flightService;

        #endregion


        #region METHODS

        public FlightsController(
            IFlightService flightService)
        {
            this._flightService = flightService;
        }

        public IActionResult GetAll(DataTableRequestModel model)
        {
            int recordsTotal = 0;
            int recordsFiltered = 0;
            IList<Flight> flights = (IList<Flight>)_flightService.GetAll(model, out recordsTotal, out recordsFiltered);

            DataTableResponseModel<Flight> flightsResponse = new DataTableResponseModel<Flight>(flights, model.draw, recordsFiltered, recordsTotal);

            return Ok(flightsResponse);
        }

        #endregion
    }
}
