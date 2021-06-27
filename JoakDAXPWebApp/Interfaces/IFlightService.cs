using JoakDAXPWebApp.Entities;
using JoakDAXPWebApp.Models.DataTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JoakDAXPWebApp.Interfaces
{
    public interface IFlightService
    {
        #region METHODS

        /// <summary>
        /// Allow to insert the data for a flight on the database.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool InsertFlight(Flight data);

        /// <summary>
        /// Retrieve data to show on a table with search, sort and pagination included.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="recordsTotal"></param>
        /// <param name="recordsFiltered"></param>
        /// <returns></returns>
        IList<Flight> GetAll(DataTableRequestModel model, out int recordsTotal, out int recordsFiltered);

        #endregion
    }
}
