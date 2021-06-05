using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using XPlaneUDPExchange.Helpers;
using XPlaneUDPExchange.Interfaces;

namespace XPlaneUDPExchange.Model
{
    public class XPlaneUDPExchange : IXPlaneUDPExchange, IDisposable
    {
        #region PRIVATE_MEMBER_PROPERTIES

        /// <summary>
        /// Udp client needed to open socket
        /// </summary>
        private UdpClient _udpClient;

        /// <summary>
        /// Represent a network IP address and a port to listen for connections.
        /// </summary>
        private IPEndPoint _ipEndpoint;

        #endregion

        #region PUBLIC_METHODS

        /// <summary>
        /// Method used to configure the dll with general parameters such as the path where log files will be stored, if debug mode is enabled or not, or the udp port to open an UDP socket.
        /// </summary>
        /// <param name="logPath">Path where log files will be stored.</param>
        /// <param name="debugMode"><c>True</c> to enable debug mode. <c>False</c> to disable it.</param>
        /// <param name="udpPort">Number of port where UDP socket to connect to the simulator will be opened.</param>
        /// <returns></returns>
        public bool ConfigureDll(string logPath, bool debugMode, int udpPort)
        {
            bool result = false;
            try
            {
                // Set parameters
                Config.logPath = !string.IsNullOrEmpty(logPath) ? logPath : Path.GetDirectoryName(Assembly.GetCallingAssembly().GetName().FullName);
                Config.debugMode = debugMode;
                Config.udpPort = udpPort;

                LogHelper.Func_WriteEventInLogFile(DateTime.Now.ToLocalTime(), Enum_EventTypes.Debug, "",
                    MethodBase.GetCurrentMethod().DeclaringType.FullName + "." + MethodBase.GetCurrentMethod().Name + "()", "Get Config Parameters: Ok",
                    string.Format("Log Path: {0} | Debug mode: {1} | UDP Port: {2}", logPath, debugMode, udpPort));

                LogHelper.Func_WriteEventInLogFile(DateTime.Now.ToLocalTime(), Enum_EventTypes.Info, "", MethodBase.GetCurrentMethod().DeclaringType.FullName + "." + MethodBase.GetCurrentMethod().Name + "()",
                    "Configuration applied: Ok", "Configuration applied successfully");

                result = true;
            }
            catch(Exception exception1)
            {
                LogHelper.Func_WriteEventInLogFile(DateTime.Now.ToLocalTime(), Enum_EventTypes.Error, "", string.Format("{0}.{1}()", MethodBase.GetCurrentMethod().DeclaringType.FullName, MethodBase.GetCurrentMethod().Name), "GeneralException",
                  "Source = " + exception1.Source.Replace("'", "''") + ", Message = " + exception1.Message.Replace("'", "''"));
            }
            return result;
        }

        /// <summary>
        /// Return the library assembly version. Useful to test that the library has been referenced successfully.
        /// </summary>
        /// <returns>String containing the library version.</returns>
        public string GetDllVersion()
        {
            string result = "";

            LogHelper.Func_WriteEventInLogFile(DateTime.Now.ToLocalTime(), Enum_EventTypes.Debug, "", MethodBase.GetCurrentMethod().DeclaringType.FullName + "." + MethodBase.GetCurrentMethod().Name + "()", "Get Dll Version: ",
                "Trying to get dll version...");
            try
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);

                result = string.Format("{0} V{1}", assembly.GetName().Name, fvi.FileVersion);

                LogHelper.Func_WriteEventInLogFile(DateTime.Now.ToLocalTime(), Enum_EventTypes.Info, "", MethodBase.GetCurrentMethod().DeclaringType.FullName + "." + MethodBase.GetCurrentMethod().Name + "()", "Get Dll Version: Ok",
                    string.Format("Version: {0}", result));
            }
            catch (Exception exception1)
            {
                LogHelper.Func_WriteEventInLogFile(DateTime.Now.ToLocalTime(), Enum_EventTypes.Error, "", string.Format("{0}.{1}()", MethodBase.GetCurrentMethod().DeclaringType.FullName, MethodBase.GetCurrentMethod().Name), "GeneralException",
                  "Source = " + exception1.Source.Replace("'", "''") + ", Message = " + exception1.Message.Replace("'", "''"));
            }

            return result;
        }

        /// <summary>
        /// Initialize the connection with the simulator.
        /// </summary>
        /// <returns><c>True</c> if UDP socket has been opened successfully. <c>False</c> in other case.</returns>
        public bool InitConnection()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Check if the connection with the simulator is running or not.
        /// </summary>
        /// <returns><c>True</c> if connection is still running. <c>False</c> if connection is not running.</returns>
        public bool CheckConnectionIsRunning()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Finish a running connection.
        /// </summary>
        /// <returns><c>True</c> if connection ended successfully. <c>False</c> in case of any errors or if connection is not initialized.</returns>
        public bool EndConnection()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            if (_udpClient != null)
            {
                _udpClient.Dispose();
            }
        }

        #endregion
    }
}
