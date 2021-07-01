using JoakDAXPWebApp.Entities;
using System.Collections.Generic;

namespace JoakDAXPWebApp.Interfaces
{
    public interface ILicenseInfoService
    {
        #region METHODS

        /// <summary>
        /// Retrieve all license info to show on a view ordered by its name ASC.
        /// </summary>
        /// <returns></returns>
        IList<LicenseInfo> GetAll();

        #endregion
    }
}
