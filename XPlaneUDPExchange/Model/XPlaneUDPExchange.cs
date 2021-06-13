using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading;
using XPlaneUDPExchange.Helpers;
using XPlaneUDPExchange.Interfaces;
using XPlaneUDPExchange.Model.Data;
using XPlaneUDPExchange.Model.Events;

namespace XPlaneUDPExchange.Model
{
    public class XPlaneUDPExchange : IXPlaneUDPExchange, IDisposable
    {
        #region PROPERTIES

        /// <summary>
        /// <c>True</c> when service is listening for UDP data. <c>False</c> when it has been stopped or not initialized.
        /// </summary>
        private bool _serviceListening;

        /// <summary>
        /// <c>True</c> when service has been manually finished by an user. <c>False</c> in other case.
        /// </summary>
        private bool _manuallyFinished;

        /// <summary>
        /// Udp client needed to open socket
        /// </summary>
        private UdpClient _udpClient;

        /// <summary>
        /// Represent a network IP address and a port to listen for connections.
        /// </summary>
        private IPEndPoint _ipEndpoint;

        /// <summary>
        /// X-Plane data received event handler.
        /// </summary>
        public static event EventHandler<XPlaneDataReceivedEvent> XPDataReceived = null;

        #endregion

        #region CONSTRUCTOR

        public XPlaneUDPExchange()
        {
            this._serviceListening = false;
            this._manuallyFinished = false;
        }

        ~XPlaneUDPExchange()
        {
            this.Dispose();
        }

        public void Dispose()
        {
            if (_udpClient != null)
            {
                _udpClient.Dispose();
            }
            _manuallyFinished = false;
            _serviceListening = false;
        }

        #endregion

        #region INTERFACE_METHODS

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
            catch (Exception exception1)
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
            bool result = false;

            try
            {
                LogHelper.Func_WriteEventInLogFile(DateTime.Now.ToLocalTime(), Enum_EventTypes.Debug, "",
                    MethodBase.GetCurrentMethod().DeclaringType.FullName + "." + MethodBase.GetCurrentMethod().Name + "()", "Init Connection Simulator: ", string.Format("Initializing UDP Socket on port: {0}...", Config.udpPort));

                // Create a new thread to run UDP server
                //StartUdpListener();
                ThreadHelper.Func_Create_Start_Thread(StartUdpListener, "Thread_UDP_Listener", true);

                result = true;

                LogHelper.Func_WriteEventInLogFile(DateTime.Now.ToLocalTime(), Enum_EventTypes.Info, "",
                   MethodBase.GetCurrentMethod().DeclaringType.FullName + "." + MethodBase.GetCurrentMethod().Name + "()", "Init Connection Simulator: Ok", "Successfully run thread for start udp listener.");
            }
            catch (Exception exception1)
            {
                LogHelper.Func_WriteEventInLogFile(DateTime.Now.ToLocalTime(), Enum_EventTypes.Error, "", string.Format("{0}.{1}()", MethodBase.GetCurrentMethod().DeclaringType.FullName, MethodBase.GetCurrentMethod().Name), "GeneralException",
                  "Source = " + exception1.Source.Replace("'", "''") + ", Message = " + exception1.Message.Replace("'", "''"));
            }

            return result;
        }

        /// <summary>
        /// Check if the connection with the simulator is running or not.
        /// </summary>
        /// <returns><c>True</c> if connection is still running. <c>False</c> if connection is not running.</returns>
        public bool CheckConnectionIsRunning()
        {
            bool result = false;

            try
            {
                result = _serviceListening;
                LogHelper.Func_WriteEventInLogFile(DateTime.Now.ToLocalTime(), Enum_EventTypes.Info, "",
                 MethodBase.GetCurrentMethod().DeclaringType.FullName + "." + MethodBase.GetCurrentMethod().Name + "()", "Check Connection Running: Ok", string.Format("UDP listener running? {0}.", _serviceListening.ToString()));
            }
            catch (Exception exception1)
            {
                LogHelper.Func_WriteEventInLogFile(DateTime.Now.ToLocalTime(), Enum_EventTypes.Error, "", string.Format("{0}.{1}()", MethodBase.GetCurrentMethod().DeclaringType.FullName, MethodBase.GetCurrentMethod().Name), "GeneralException",
                  "Source = " + exception1.Source.Replace("'", "''") + ", Message = " + exception1.Message.Replace("'", "''"));
            }

            return result;
        }

        /// <summary>
        /// Finish a running connection.
        /// </summary>
        /// <returns><c>True</c> if connection ended successfully. <c>False</c> in case of any errors or if connection is not initialized.</returns>
        public bool EndConnection()
        {
            bool result = false;

            try
            {
                _manuallyFinished = true;
                _serviceListening = false;
                LogHelper.Func_WriteEventInLogFile(DateTime.Now.ToLocalTime(), Enum_EventTypes.Info, "",
                 MethodBase.GetCurrentMethod().DeclaringType.FullName + "." + MethodBase.GetCurrentMethod().Name + "()", "End Connection: Ok", "Connection stopped by an user.");
            }
            catch (Exception exception1)
            {
                LogHelper.Func_WriteEventInLogFile(DateTime.Now.ToLocalTime(), Enum_EventTypes.Error, "", string.Format("{0}.{1}()", MethodBase.GetCurrentMethod().DeclaringType.FullName, MethodBase.GetCurrentMethod().Name), "GeneralException",
                  "Source = " + exception1.Source.Replace("'", "''") + ", Message = " + exception1.Message.Replace("'", "''"));
            }

            return result;
        }

        #endregion

        #region PRIVATE_METHODS

        /// <summary>
        /// Start the UDP client to receive data from the simulator.
        /// </summary>
        private void StartUdpListener()
        {
            _udpClient = new UdpClient(Config.udpPort);
            _ipEndpoint = new IPEndPoint(IPAddress.Any, Config.udpPort);
            byte[] bytesReceived;
            IEnumerable<XPlaneData> data;
            try
            {
                LogHelper.Func_WriteEventInLogFile(DateTime.Now.ToLocalTime(), Enum_EventTypes.Info, "",
                  MethodBase.GetCurrentMethod().DeclaringType.FullName + "." + MethodBase.GetCurrentMethod().Name + "()", "Start UDP Listener: ", string.Format("Wating form messages: Ip = 'Any', Port = {0}.", Config.udpPort));
                _serviceListening = true;
                _manuallyFinished = false;
                while (_serviceListening)
                {

                    try
                    {
                        bytesReceived = _udpClient.Receive(ref _ipEndpoint);
                        // TODO: Process data
                        data = ProcessUdpDataReceived(bytesReceived);
                        if (data != null && data.GetType() != typeof(DataUnknown))
                        {
                            LogHelper.Func_WriteEventInLogFile(DateTime.Now.ToLocalTime(), Enum_EventTypes.Debug, "",
                            MethodBase.GetCurrentMethod().DeclaringType.FullName + "." + MethodBase.GetCurrentMethod().Name + "()", "Start UDP Listener: Ok", string.Format("Message received: Ip = {0}, Port = {1}.",
                            _ipEndpoint.Address.ToString(), _ipEndpoint.Port.ToString()));
                            // TODO: Fire event that data has been received.
                            OnDataReceived(null, new XPlaneDataReceivedEvent(data));
                        }
                        else
                        {
                            LogHelper.Func_WriteEventInLogFile(DateTime.Now.ToLocalTime(), Enum_EventTypes.Debug, "",
                             MethodBase.GetCurrentMethod().DeclaringType.FullName + "." + MethodBase.GetCurrentMethod().Name + "()", "Start UDP Listener: Ok", string.Format("Unrecognized Message received or Null: Ip = {0}, Port = {1}.",
                             _ipEndpoint.Address.ToString(), _ipEndpoint.Port.ToString()));
                        }
                    }
                    catch (Exception udpDataException1)
                    {
                        LogHelper.Func_WriteEventInLogFile(DateTime.Now.ToLocalTime(), Enum_EventTypes.Error, "", string.Format("{0}.{1}()", MethodBase.GetCurrentMethod().DeclaringType.FullName, MethodBase.GetCurrentMethod().Name), "UdpDataException",
                        "Source = " + udpDataException1.Source.Replace("'", "''") + ", Message = " + udpDataException1.Message.Replace("'", "''"));
                    }
                    Thread.Sleep(100);
                }
            }
            catch (SocketException socketException1)
            {
                _serviceListening = false;
                LogHelper.Func_WriteEventInLogFile(DateTime.Now.ToLocalTime(), Enum_EventTypes.Error, "", string.Format("{0}.{1}()", MethodBase.GetCurrentMethod().DeclaringType.FullName, MethodBase.GetCurrentMethod().Name), "SocketException",
                  "Source = " + socketException1.Source.Replace("'", "''") + ", Message = " + socketException1.Message.Replace("'", "''"));
            }
            catch (Exception exception1)
            {
                _serviceListening = false;
                LogHelper.Func_WriteEventInLogFile(DateTime.Now.ToLocalTime(), Enum_EventTypes.Error, "", string.Format("{0}.{1}()", MethodBase.GetCurrentMethod().DeclaringType.FullName, MethodBase.GetCurrentMethod().Name), "GeneralException",
                  "Source = " + exception1.Source.Replace("'", "''") + ", Message = " + exception1.Message.Replace("'", "''"));
            }
            finally
            {
                if (!_serviceListening && !_manuallyFinished)
                {
                    // Relaunch UDP listener
                    StartUdpListener();
                }
            }
        }

        /// <summary>
        /// Trigger events.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void OnDataReceived(object sender, XPlaneDataReceivedEvent e)
        {
            if (XPDataReceived != null)
            {
                XPDataReceived(null, e);
            }
        }

        #endregion

        #region PROCESS_DATA_METHODS

        /// <summary>
        /// Filter the packets that wil be processed.
        /// </summary>
        /// <param name="bytesReceived">Array of <c>byte</c> with data from simulator.</param>
        /// <returns></returns>
        private IList<XPlaneData> ProcessUdpDataReceived(byte[] bytesReceived)
        {
            IList<XPlaneData> data = new List<XPlaneData>();
            try
            {
                int pos = 0;
                string header = Encoding.UTF8.GetString(bytesReceived, pos, 4); // Header of data packet is from position 0 to 4 (5 bytes)
                pos += 5;

                switch (header.ToUpper())
                {
                    case "DATA":
                        data = ProcessDataPackets(bytesReceived, pos);
                        break;
                    default:
                        // Do not process other messages.
                        break;
                }
            }
            catch (Exception exception1)
            {
                LogHelper.Func_WriteEventInLogFile(DateTime.Now.ToLocalTime(), Enum_EventTypes.Error, "", string.Format("{0}.{1}()", MethodBase.GetCurrentMethod().DeclaringType.FullName, MethodBase.GetCurrentMethod().Name), "GeneralException",
                  "Source = " + exception1.Source.Replace("'", "''") + ", Message = " + exception1.Message.Replace("'", "''"));
            }

            return data;
        }

        /// <summary>
        /// Process data packets received from simulator individually based on the group index and converting the values to the respective objects.
        /// </summary>
        /// <param name="bytesReceived"></param>
        /// <param name="pos"></param>
        /// <returns></returns>
        private IList<XPlaneData> ProcessDataPackets(byte[] bytesReceived, int pos)
        {
            IList<XPlaneData> data = new List<XPlaneData>();
            try
            {
                int id = -1;
                float[] values = new float[8];

                while (pos < bytesReceived.Length)
                {
                    // Get index to know the type
                    id = BitConverter.ToInt32(bytesReceived, pos);
                    pos += 4;

                    //values.Add(BitConverter.ToSingle(bytesReceived, pos));
                    for (int k = 0; k < 8; k++)
                    {
                        values[k] = BitConverter.ToSingle(bytesReceived, pos);
                        pos += 4;
                    }

                    // Get index and process values
                    switch (id)
                    {
                        case 0:
                            data.Add(FrameRate(values));
                            break;
                        case 1:
                            data.Add(Times(values));
                            break;
                        case 3:
                            data.Add(Speeds(values));
                            break;
                        case 4:
                            data.Add(MachVviGLoad(values));
                            break;
                        case 5:
                            data.Add(Weather(values));
                            break;
                        case 6:
                            data.Add(Atmosphere(values));
                            break;
                        case 7:
                            data.Add(SystemPressures(values));
                            break;
                        case 13:
                            data.Add(TrimFlapsSlatsSpeedBrakes(values));
                            break;
                        case 14: 
                            data.Add(LandingGearBrakes(values));
                            break;
                        case 17:
                            data.Add(PitchRollHeading(values));
                            break;
                        case 20:
                            data.Add(LatLogAlt(values));
                            break;
                        case 21:
                            data.Add(LocVelDist(values));
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception exception1)
            {
                List<float> values = new List<float>(8);
                LogHelper.Func_WriteEventInLogFile(DateTime.Now.ToLocalTime(), Enum_EventTypes.Error, "", string.Format("{0}.{1}()", MethodBase.GetCurrentMethod().DeclaringType.FullName, MethodBase.GetCurrentMethod().Name), "GeneralException",
                  "Source = " + exception1.Source.Replace("'", "''") + ", Message = " + exception1.Message.Replace("'", "''"));
            }
            return data;
        }

        /// <summary>
        /// Set frame rate values to its object.
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        private XPlaneData FrameRate(float[] values)
        {
            DataFrameRate result = new DataFrameRate();
            try
            {
                result.FrameRate = values[0];
                result.FrameRateSim = values[1];
                result.FrameTime = values[2];
                result.CpuTime = values[3];
                result.GpuTime = values[4];
                result.GroundRatio = values[5];
                result.FlitRatio = values[6];
            }
            catch (Exception exception1)
            {
                LogHelper.Func_WriteEventInLogFile(DateTime.Now.ToLocalTime(), Enum_EventTypes.Error, "", string.Format("{0}.{1}()", MethodBase.GetCurrentMethod().DeclaringType.FullName, MethodBase.GetCurrentMethod().Name), "ParseException",
                  "Source = " + exception1.Source.Replace("'", "''") + ", Message = " + exception1.Message.Replace("'", "''"));
            }
            return result;
        }

        /// <summary>
        /// Set times values into an object.
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        private XPlaneData Times(float[] values)
        {
            DataTimes result = new DataTimes();
            try
            {
                result.RealTime = values[0];
                result.TotalTime = values[1];
                result.MissionTime = values[2];
                result.TimerTime = values[3];
                result.ZuluTime = values[5];
                result.LocalTime = values[6];
                result.HobbsTime = values[7];

                /*TimeSpan zuluTime = TimeSpan.FromHours(result.ZuluTime);
                DateTime zuluDT = DateTime.UtcNow;
                zuluDT.AddTicks(zuluTime.Ticks);

                DateTime localDT = zuluDT.ToLocalTime();
                TimeSpan localTime = TimeSpan.FromHours(result.LocalTime);*/
            }
            catch (Exception exception1)
            {
                LogHelper.Func_WriteEventInLogFile(DateTime.Now.ToLocalTime(), Enum_EventTypes.Error, "", string.Format("{0}.{1}()", MethodBase.GetCurrentMethod().DeclaringType.FullName, MethodBase.GetCurrentMethod().Name), "ParseException",
                  "Source = " + exception1.Source.Replace("'", "''") + ", Message = " + exception1.Message.Replace("'", "''"));
            }
            return result;
        }

        /// <summary>
        /// Convert float values into a <c>DataSpeed</c> object.
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        private XPlaneData Speeds(float[] values)
        {
            DataSpeed result = new DataSpeed();
            try
            {
                result.VIndKts = values[0];
                result.VIndKeas = values[1];
                result.VTrueKts = values[2];
                result.VTrueKtgs = values[3];
                result.VIndMph = values[4];
                result.VTrueMph = values[5];
                result.VTrueMphgs = values[6];
            }
            catch (Exception exception1)
            {
                LogHelper.Func_WriteEventInLogFile(DateTime.Now.ToLocalTime(), Enum_EventTypes.Error, "", string.Format("{0}.{1}()", MethodBase.GetCurrentMethod().DeclaringType.FullName, MethodBase.GetCurrentMethod().Name), "ParseException",
                  "Source = " + exception1.Source.Replace("'", "''") + ", Message = " + exception1.Message.Replace("'", "''"));
            }
            return result;
        }

        /// <summary>
        /// Convert values received from the simulator to a <c>DataMachVviGLoad</c> object.
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        private XPlaneData MachVviGLoad(float[] values)
        {
            DataMachVviGLoad result = new DataMachVviGLoad();
            try
            {
                result.Mach = values[0];
                result.VerticalSpeed = values[1];
                result.GLoadNormal = values[2];
                result.GLoadAxial = values[3];
                result.GLoadSide = values[4];
            }
            catch (Exception exception1)
            {
                LogHelper.Func_WriteEventInLogFile(DateTime.Now.ToLocalTime(), Enum_EventTypes.Error, "", string.Format("{0}.{1}()", MethodBase.GetCurrentMethod().DeclaringType.FullName, MethodBase.GetCurrentMethod().Name), "ParseException",
                  "Source = " + exception1.Source.Replace("'", "''") + ", Message = " + exception1.Message.Replace("'", "''"));
            }
            return result;
        }

        /// <summary>
        /// Convert float values into a <c>DataWeather</c> object.
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        private XPlaneData Weather(float[] values)
        {
            DataWeather result = new DataWeather();
            try
            {
                result.SeaLevelPressure = values[0];
                result.SeaLevelTemperature = values[1];
                result.WindSpeed = values[2];
                result.WindDirection = values[3];
                result.Turbulence = values[4];
                result.Precipitation = values[5];
                result.Hail = values[6];
            }
            catch (Exception exception1)
            {
                LogHelper.Func_WriteEventInLogFile(DateTime.Now.ToLocalTime(), Enum_EventTypes.Error, "", string.Format("{0}.{1}()", MethodBase.GetCurrentMethod().DeclaringType.FullName, MethodBase.GetCurrentMethod().Name), "ParseException",
                  "Source = " + exception1.Source.Replace("'", "''") + ", Message = " + exception1.Message.Replace("'", "''"));
            }
            return result;
        }

        /// <summary>
        /// Convert float values into a <c>DataAtmosphere</c> object.
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        private XPlaneData Atmosphere(float[] values)
        {
            DataAtmosphere result = new DataAtmosphere();
            try
            {
                result.AmbientPressureHg = values[0];
                result.AmbientTemperatureDegC = values[1];
                result.LeadingEdgeTemperatureDegC = values[2];
                result.AirDensityRatio = values[3];
                result.Akts = values[4];
                result.QPsf = values[5];
                result.GravityFtSecSqrd = values[6];
            }
            catch (Exception exception1)
            {
                LogHelper.Func_WriteEventInLogFile(DateTime.Now.ToLocalTime(), Enum_EventTypes.Error, "", string.Format("{0}.{1}()", MethodBase.GetCurrentMethod().DeclaringType.FullName, MethodBase.GetCurrentMethod().Name), "ParseException",
                  "Source = " + exception1.Source.Replace("'", "''") + ", Message = " + exception1.Message.Replace("'", "''"));
            }
            return result;
        }

        /// <summary>
        /// Convert values received from the simulator to a <c>DataSystemPressures</c> object.
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        private XPlaneData SystemPressures(float[] values)
        {
            DataSystemPressures result = new DataSystemPressures();
            try
            {
                result.BarometricPressure = values[0];
                result.Edens = values[1];
                result.Vacum = values[2];
                result.Elec = values[3];
                result.AHRS = values[4];
            }
            catch (Exception exception1)
            {
                LogHelper.Func_WriteEventInLogFile(DateTime.Now.ToLocalTime(), Enum_EventTypes.Error, "", string.Format("{0}.{1}()", MethodBase.GetCurrentMethod().DeclaringType.FullName, MethodBase.GetCurrentMethod().Name), "ParseException",
                  "Source = " + exception1.Source.Replace("'", "''") + ", Message = " + exception1.Message.Replace("'", "''"));
            }
            return result;
        }

        /// <summary>
        /// Convert values received from the simulator to a <c>DataTrimFlapsSlatsSpeedBrakes</c> object.
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        private XPlaneData TrimFlapsSlatsSpeedBrakes(float[] values)
        {
            DataTrimFlapsSlatsSpeedBrakes result = new DataTrimFlapsSlatsSpeedBrakes();
            try
            {
                result.ElevatorTrim = values[0];
                result.AileronTrim = values[1];
                result.RuddlerTrim = values[2];
                result.Flap = values[3];
                result.FlapPosition = values[4];
                result.SlatPosition = values[5];
                result.SpeedBrake = values[6];
                result.SpeedBrakePosition = values[7];
            }
            catch (Exception exception1)
            {
                LogHelper.Func_WriteEventInLogFile(DateTime.Now.ToLocalTime(), Enum_EventTypes.Error, "", string.Format("{0}.{1}()", MethodBase.GetCurrentMethod().DeclaringType.FullName, MethodBase.GetCurrentMethod().Name), "ParseException",
                  "Source = " + exception1.Source.Replace("'", "''") + ", Message = " + exception1.Message.Replace("'", "''"));
            }
            return result;
        }

        /// <summary>
        /// Convert values received from the simulator to a <c>DataLandingGearBrakes</c> object.
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        private XPlaneData LandingGearBrakes(float[] values)
        {
            DataLandingGearBrakes result = new DataLandingGearBrakes();
            try
            {
                result.GearExtended = (int)values[0];
                result.WBrake = values[1];
                result.LeftBrake = values[2];
                result.RightBrake = values[3];
            }
            catch (Exception exception1)
            {
                LogHelper.Func_WriteEventInLogFile(DateTime.Now.ToLocalTime(), Enum_EventTypes.Error, "", string.Format("{0}.{1}()", MethodBase.GetCurrentMethod().DeclaringType.FullName, MethodBase.GetCurrentMethod().Name), "ParseException",
                  "Source = " + exception1.Source.Replace("'", "''") + ", Message = " + exception1.Message.Replace("'", "''"));
            }
            return result;
        }

        /// <summary>
        /// Convert values received from the simulator to a <c>DataAttitude</c> object.
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        private XPlaneData PitchRollHeading(float[] values)
        {
            DataAttitude result = new DataAttitude();
            try
            {
                result.Pitch = values[0];
                result.Roll = values[1];
                result.HeadingTrue = values[2];
                result.HeadingMagnetic = values[3];
            }
            catch (Exception exception1)
            {
                LogHelper.Func_WriteEventInLogFile(DateTime.Now.ToLocalTime(), Enum_EventTypes.Error, "", string.Format("{0}.{1}()", MethodBase.GetCurrentMethod().DeclaringType.FullName, MethodBase.GetCurrentMethod().Name), "ParseException",
                  "Source = " + exception1.Source.Replace("'", "''") + ", Message = " + exception1.Message.Replace("'", "''"));
            }
            return result;
        }

        /// <summary>
        /// Convert values received from the simulator to a <c>DataPosition</c> object.
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        private XPlaneData LatLogAlt(float[] values)
        {
            DataPosition result = new DataPosition();
            try
            {
                result.Latitude = values[0];
                result.Longitude = values[1];
                result.AltitudeSeaLevel = values[2];
                result.AltitudeGroundLevel = values[3];
                result.Runway = values[4];
                result.AltitudeIndicated = values[5];
                result.LatitudeNormalized = values[6];
                result.LongitudeNormalized = values[7];
            }
            catch (Exception exception1)
            {
                LogHelper.Func_WriteEventInLogFile(DateTime.Now.ToLocalTime(), Enum_EventTypes.Error, "", string.Format("{0}.{1}()", MethodBase.GetCurrentMethod().DeclaringType.FullName, MethodBase.GetCurrentMethod().Name), "ParseException",
                  "Source = " + exception1.Source.Replace("'", "''") + ", Message = " + exception1.Message.Replace("'", "''"));
            }
            return result;
        }

        /// <summary>
        /// Convert values received from the simulator to a <c>DataLocationVelocityDistanceTraveled</c> object.
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        private XPlaneData LocVelDist(float[] values)
        {
            DataLocationVelocityDistanceTraveled result = new DataLocationVelocityDistanceTraveled();
            try
            {
                result.X = values[0];
                result.Y = values[1];
                result.Z = values[2];
                result.vX = values[3];
                result.vY = values[4];
                result.vZ = values[5];
                result.DistanceFeet = values[6];
                result.DistanceNm = values[7];
            }
            catch (Exception exception1)
            {
                LogHelper.Func_WriteEventInLogFile(DateTime.Now.ToLocalTime(), Enum_EventTypes.Error, "", string.Format("{0}.{1}()", MethodBase.GetCurrentMethod().DeclaringType.FullName, MethodBase.GetCurrentMethod().Name), "ParseException",
                  "Source = " + exception1.Source.Replace("'", "''") + ", Message = " + exception1.Message.Replace("'", "''"));
            }
            return result;
        }


        private XPlaneData Unknown(int id)
        {
            return new DataUnknown();
        }

        #endregion
    }
}
