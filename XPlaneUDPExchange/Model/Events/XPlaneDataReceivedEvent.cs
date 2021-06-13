using System;
using System.Collections.Generic;
using XPlaneUDPExchange.Model.Data;

namespace XPlaneUDPExchange.Model.Events
{
    public class XPlaneDataReceivedEvent : EventArgs
    {
        #region PUBLIC_MEMBER_PROPERTIES

        /// <summary>
        /// UTC Datetime when data was received from the simulator.
        /// </summary>
        public DateTime receptionDt { get; set; }

        /// <summary>
        /// Data received from X-Plane.
        /// </summary>
        public IEnumerable<XPlaneData> data { get; set; }

        #endregion

        #region CONSTRUCTOR

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="data"></param>
        public XPlaneDataReceivedEvent(IEnumerable<XPlaneData> data)
        {
            this.receptionDt = DateTime.UtcNow;
            this.data = data;
        }

        #endregion
    }
}
