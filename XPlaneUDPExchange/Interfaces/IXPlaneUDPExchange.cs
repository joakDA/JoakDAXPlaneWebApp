using System;
using System.Collections.Generic;
using System.Text;

namespace XPlaneUDPExchange.Interfaces
{
    public interface IXPlaneUDPExchange
    {
        /// <summary>
        /// Method used to configure the dll with general parameters such as the path where log files will be stored, if debug mode is enabled or not, or the udp port to open an UDP socket.
        /// </summary>
        /// <param name="logPath">Path where log files will be stored.</param>
        /// <param name="debugMode"><c>True</c> to enable debug mode. <c>False</c> to disable it.</param>
        /// <param name="udpPort">Number of port where UDP socket to connect to the simulator will be opened.</param>
        /// <returns></returns>
        bool ConfigureDll(string logPath, bool debugMode, int udpPort);

        /// <summary>
        /// Return the library assembly version. Useful to test that the library has been referenced successfully.
        /// </summary>
        /// <returns>String containing the library version.</returns>
        string GetDllVersion();

        /// <summary>
        /// Initialize the connection with the simulator.
        /// </summary>
        /// <returns><c>True</c> if UDP socket has been opened successfully. <c>False</c> in other case.</returns>
        bool InitConnection();

        /// <summary>
        /// Check if the connection with the simulator is running or not.
        /// </summary>
        /// <returns><c>True</c> if connection is still running. <c>False</c> if connection is not running.</returns>
        bool CheckConnectionIsRunning();

        /// <summary>
        /// Finish a running connection.
        /// </summary>
        /// <returns><c>True</c> if connection ended successfully. <c>False</c> in case of any errors or if connection is not initialized.</returns>
        bool EndConnection();
    }
}
