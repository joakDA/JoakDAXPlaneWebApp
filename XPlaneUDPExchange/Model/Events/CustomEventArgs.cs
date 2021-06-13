using System;
using XPlaneUDPExchange.Helpers;

namespace XPlaneUDPExchange.Model.Events
{
    public class CustomEventArgs : EventArgs
    {
        #region PUBLIC_MEMBER_PROPERTIES

        /// <summary>
        /// String with the message to sent to event listener.
        /// </summary>
        public string Message { get; set; }

        public Enum_EventTypes EventType { get; set; }

        #endregion
    }
}
