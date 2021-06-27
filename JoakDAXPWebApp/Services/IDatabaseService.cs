using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JoakDAXPWebApp.Services
{
    public interface IDatabaseService
    {
        #region METHODS

        /// <summary>
        /// Convert the numeric index of the column to the column name to make sort possible on the
        /// datable server side.
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        string ConvertColumnIndexToName(int column);

        #endregion
    }
}
