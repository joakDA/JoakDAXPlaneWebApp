using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JoakDAXPWebApp.Entities
{
    public class LicenseInfo : BaseEntity
    {
        #region PROPERTIES

        /// <summary>
        /// Unique identifier of the license on the database. Primary key
        /// </summary>
        public int Id { get; set;}

        /// <summary>
        /// Name of the library.
        /// </summary>
        public string LibraryName { get; set; }

        /// <summary>
        /// License name.
        /// </summary>
        public string LicenseName { get; set; }

        /// <summary>
        /// Text of license to display.
        /// </summary>
        public string LicenseText { get; set; }

        /// <summary>
        /// <c>true</c> to show this library. <c>false</c> to hide it.
        /// </summary>
        public bool Enabled { get; set; }

        #endregion
    }
}
