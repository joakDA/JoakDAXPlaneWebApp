using System;
using XPlaneUDPExchange.Helpers;

namespace XPlaneUDPExchange.Model
{
    public class CustomEventArgs : EventArgs
    {
        #region PUBLIC_MEMBER_PROPERTIES

        /// <summary>
        /// String with the message to sent to event listener.
        /// </summary>
        public string message { get; set; }

        public Enum_EventTypes eventType { get; set; }

        #endregion
    }
}
