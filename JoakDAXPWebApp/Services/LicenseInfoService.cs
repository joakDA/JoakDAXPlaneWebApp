using JoakDAXPWebApp.Data;
using JoakDAXPWebApp.Entities;
using JoakDAXPWebApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Reflection;
using XPlaneUDPExchange.Helpers;

namespace JoakDAXPWebApp.Services
{
    public class LicenseInfoService : ILicenseInfoService
    {
        #region PROPERTIES

        private ApplicationDbContext _context;

        #endregion

        #region METHODS

        /// <summary>
        /// Constructor using DI.
        /// </summary>
        /// <param name="context"></param>
        public LicenseInfoService(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieve all license info to show on a view ordered by its name ASC.
        /// </summary>
        /// <returns></returns>
        public IList<LicenseInfo> GetAll()
        {
            IList<LicenseInfo> result;
            try
            {
                IQueryable<LicenseInfo> licensesData;

                // Get licenses from database
                licensesData = _context.LicenseInfo.AsQueryable();

                // Order by Library Name and store on result variable
                result = licensesData.Where(li => li.Enabled == true).OrderBy("LibraryName ASC").ToList();
            }catch (Exception exception1)
            {
                result = new List<LicenseInfo>();
                LogHelper.Func_WriteEventInLogFile(DateTime.Now.ToLocalTime(), Enum_EventTypes.Error, "", string.Format("{0}.{1}()", MethodBase.GetCurrentMethod().DeclaringType.FullName, MethodBase.GetCurrentMethod().Name), "GeneralException",
                 "Source = " + exception1.Source.Replace("'", "''") + ", Message = " + exception1.Message.Replace("'", "''"));
            }

            return result;
        }

        #endregion
    }
}
