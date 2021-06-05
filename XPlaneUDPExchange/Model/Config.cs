using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace XPlaneUDPExchange.Model
{
    internal static class Config
    {
        #region INTERNAL_STATIC_PROPERTIES

        /// <summary>
        /// Path to directory where write log file.
        /// </summary>
        internal static string logPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), Assembly.GetExecutingAssembly().GetName().Name);

        /// <summary>
        /// If <c>true</c> write to log debug messages. <c>False</c> to only write in log error or info messages.
        /// </summary>
        internal static bool debugMode = false;

        #endregion

        #region XPLANE_CONFIG_PARAMETERS

        /// <summary>
        /// Number of port between 1 to 65532 to listen for UDP connections.
        /// </summary>
        internal static int udpPort;

        #endregion
    }
}
