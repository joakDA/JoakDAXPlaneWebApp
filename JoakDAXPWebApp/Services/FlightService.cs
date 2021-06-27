using JoakDAXPWebApp.Data;
using JoakDAXPWebApp.Entities;
using JoakDAXPWebApp.Interfaces;
using JoakDAXPWebApp.Models.DataTable;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Reflection;
using System.Threading.Tasks;
using XPlaneUDPExchange.Helpers;

namespace JoakDAXPWebApp.Services
{
    public class FlightService : IFlightService, IDatabaseService
    {
        #region PROPERTIES

        private ApplicationDbContext _context;

        #endregion

        #region METHODS

        /// <summary>
        /// Constructor using DI.
        /// </summary>
        /// <param name="context"></param>
        public FlightService(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Allow to insert the data for a flight on the database.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public IList<Flight> GetAll(DataTableRequestModel model, out int recordsTotal, out int recordsFiltered)
        {
            int draw = 0;
            int start = 0;
            int length = 0;
            recordsTotal = 0;
            recordsFiltered = 0;
            IQueryable<Flight> flights;
            IList<Flight> result;
            try
            {
                string search = model.search != null ? model.search.value.ToUpper() : string.Empty;
                draw = model.draw;

                // Find paging info
                start = model.start;
                length = model.length;
                // Sort
                string sortColumn = ConvertColumnIndexToName(model.order.FirstOrDefault().column);
                string sortColumnDir = model.order.FirstOrDefault().dir;

                // Get flights
                flights = _context.Flights.Include(f => f.FlightEventType).AsQueryable();

                recordsTotal = flights.Count();

                // Execute filter
                if (!string.IsNullOrEmpty(search))
                {
                    flights = flights.Where(x => x.FlightEventType.Name.ToUpper().Contains(search) || x.Location.ToUpper().Contains(search)
                    || x.Latitude.ToString().Contains(search) || x.Longitude.ToString().Contains(search) || x.DistanceFromIdeal.ToString().Contains(search) || 
                    x.GlideslopeScore.ToString().Contains(search) || x.VerticalSpeed.ToString().Contains(search) || x.MaxForce.ToString().Contains(search) ||
                    x.Pitch.ToString().Contains(search));
                }

                if (!string.IsNullOrEmpty(sortColumn) && !string.IsNullOrEmpty(sortColumnDir))
                {
                    flights = flights.OrderBy(sortColumn + " " + sortColumnDir);
                }

                recordsFiltered = flights.Count();

                flights = flights.Skip((start)).Take(length);

                result = flights.ToList();
            }
            catch(Exception exception1)
            {
                LogHelper.Func_WriteEventInLogFile(DateTime.Now.ToLocalTime(), Enum_EventTypes.Error, "", string.Format("{0}.{1}()", MethodBase.GetCurrentMethod().DeclaringType.FullName, MethodBase.GetCurrentMethod().Name), "GeneralException",
                  "Source = " + exception1.Source.Replace("'", "''") + ", Message = " + exception1.Message.Replace("'", "''"));
                recordsFiltered = 0;
                recordsTotal = 0;
                result = new List<Flight>();
            }
            return result;
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
                        columnName = "EventDateTime";
                        break;
                    case 2:
                        columnName = "FlightEventType.Name";
                        break;
                    case 3:
                        columnName = "Location";
                        break;
                    case 4:
                        columnName = "Latitude";
                        break;
                    case 5:
                        columnName = "Longitude";
                        break;
                    case 6:
                        columnName = "DistanceFromIdeal";
                        break;
                    case 7:
                        columnName = "GlideslopeScore";
                        break;
                    case 8:
                        columnName = "VerticalSpeed";
                        break;
                    case 9:
                        columnName = "MaxForce";
                        break;
                    case 10:
                        columnName = "Pitch";
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

        /// <summary>
        /// Retrieve data to show on a table with search, sort and pagination included.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="recordsTotal"></param>
        /// <param name="recordsFiltered"></param>
        public bool InsertFlight(Flight data)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
