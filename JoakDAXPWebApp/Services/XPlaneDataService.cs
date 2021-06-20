using JoakDAXPWebApp.Hubs;
using JoakDAXPWebApp.Interfaces;
using JoakDAXPWebApp.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using XPlaneUDPExchange.Helpers;
using XPlaneUDPExchange.Interfaces;
using XPlaneUDPExchange.Model;
using XPlaneUDPExchange.Model.Data;
using XPlaneUDPExchange.Model.DTO;

namespace JoakDAXPWebApp.Services
{
    public class XPlaneDataService : IXPlaneDataService
    {
        // Service Dependency Injection
        private readonly IMemoryCache _cache;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IWebHostEnvironment _env;

        //XPlaneUDPExchange
        private IXPlaneUDPExchange _xPlaneDataLibrary;

        // Store data to compare with current received data
        private XPlaneDataModel _xplaneData;

        // Declare IHubContext for sending data to browser app
        private IHubContext<XPlaneHub> _hub;

        public XPlaneDataService(IMemoryCache memoryCache, IServiceScopeFactory scopeFactory, IWebHostEnvironment env, IHubContext<XPlaneHub> hub)
        {
            _cache = memoryCache;
            _scopeFactory = scopeFactory;
            _env = env;
            _xPlaneDataLibrary = new XPlaneUDPExchange.Model.XPlaneUDPExchange();
            _xplaneData = new XPlaneDataModel();
            _hub = hub;
        }

        /// <summary>
        /// Return the model with current XPlane data.
        /// </summary>
        /// <returns></returns>
        public XPlaneDataModel GetXPlaneDataModel()
        {
            return _xplaneData;
        }

        /// <summary>
        /// Initialize the UDP library and register event handler to receive data.
        /// </summary>
        /// <returns></returns>
        public Task<bool> InitializeXPlaneUDPExchange()
        {
            try
            {
                using (IServiceScope scope = _scopeFactory.CreateScope())
                {
                    string logPath = Path.Combine(this._env.ContentRootPath, "logs");
                    if (!Directory.Exists(logPath))
                    {
                        Directory.CreateDirectory(logPath);
                    }
                    bool serviceInitialized = _xPlaneDataLibrary.ConfigureDll(logPath, true, 18001);
                    if (serviceInitialized)
                    {
                        // Set event handler to receive data
                        XPlaneUDPExchange.Model.XPlaneUDPExchange.XPDataReceived += XPlaneUDPExchange_XPDataReceived;
                        bool connectionInitialized = _xPlaneDataLibrary.InitConnection();
                        return Task.FromResult(connectionInitialized);
                    }
                    else
                    {
                        LogHelper.Func_WriteEventInLogFile(DateTime.Now.ToLocalTime(), Enum_EventTypes.Error, "", string.Format("{0}.{1}()", MethodBase.GetCurrentMethod().DeclaringType.FullName, MethodBase.GetCurrentMethod().Name), "Error",
                            "Error configuring XPlane UDP Exchange library.");
                        return Task.FromResult(false);
                    }
                }
            }catch(Exception exception1)
            {
                LogHelper.Func_WriteEventInLogFile(DateTime.Now.ToLocalTime(), Enum_EventTypes.Error, "", string.Format("{0}.{1}()", MethodBase.GetCurrentMethod().DeclaringType.FullName, MethodBase.GetCurrentMethod().Name), "GeneralException",
                           "Source = " + exception1.Source.Replace("'", "''") + ", Message = " + exception1.Message.Replace("'", "''"));
                return Task.FromResult(false);
            }
        }

        /// <summary>
        /// Event handler to receive data from simulator and convert to Dto to send to the browser.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void XPlaneUDPExchange_XPDataReceived(object sender, XPlaneUDPExchange.Model.Events.XPlaneDataReceivedEvent e)
        {
            try
            {
                foreach(XPlaneData data in e.data)
                {
                    // Depending on the data group, convert data to a Dto instance and store in global model
                    switch (data.GetGroup())
                    {
                        case Enum_DataGroup.FrameRate:
                            _xplaneData.DataFrameRate = new DtoDataFrameRate((DataFrameRate)data);
                            break;
                        case Enum_DataGroup.Times:
                            _xplaneData.DataTimes = new DtoDataTimes((DataTimes)data);
                            break;
                        case Enum_DataGroup.Speeds:
                            _xplaneData.DataSpeed = new DtoDataSpeed((DataSpeed)data);
                            break;
                        case Enum_DataGroup.MatchVVIGLoad:
                            _xplaneData.DataMachVvi = new DtoDataMachVvi((DataMachVviGLoad)data);
                            break;
                        case Enum_DataGroup.Weather:
                            _xplaneData.DataWeather = new DtoDataWeather((DataWeather)data);
                            break;
                        case Enum_DataGroup.AircraftPressures:
                            _xplaneData.DataAtmosphere = new DtoDataAtmosphere((DataAtmosphere)data);
                            break;
                        case Enum_DataGroup.SystemPressures:
                            _xplaneData.DataSystemPressures = new DtoDataSystemPressures((DataSystemPressures)data);
                            break;
                        case Enum_DataGroup.TrimFlapsSlatsSpeedBrakes:
                            _xplaneData.DataTrimFlapsSlatsSpeedBrakes = new DtoDataTrimFlapsSlatsSpeedBrakes((DataTrimFlapsSlatsSpeedBrakes)data);
                            break;
                        case Enum_DataGroup.LandingGearBrakes:
                            _xplaneData.DataLandingGearBrakes = new DtoDataLandingGearBrakes((DataLandingGearBrakes)data);
                            break;
                        case Enum_DataGroup.PitchRollHeadings:
                            _xplaneData.DataAttitude = new DtoDataAttitude((DataAttitude)data);
                            break;
                        case Enum_DataGroup.LatitudeLongitudeAltitude:
                            _xplaneData.DataPosition = new DtoDataPosition((DataPosition)data);
                            _xplaneData.DataPosition.PropertyChanged += DataPosition_PropertyChanged;
                            break;
                        case Enum_DataGroup.LocationVelocityDistanceTraveled:
                            _xplaneData.DataLocationVelocityDistanceTraveled = new DtoDataLocationVelocityDistanceTraveled((DataLocationVelocityDistanceTraveled)data);
                            break;
                        default:
                            break;
                    }
                }
                // Trigger SignalR message using hub
                _hub.Clients.All.SendAsync("xplanedata", _xplaneData);
            }catch(Exception exception1)
            {
                LogHelper.Func_WriteEventInLogFile(DateTime.Now.ToLocalTime(), Enum_EventTypes.Error, "", string.Format("{0}.{1}()", MethodBase.GetCurrentMethod().DeclaringType.FullName, MethodBase.GetCurrentMethod().Name), "GeneralException",
                           "Source = " + exception1.Source.Replace("'", "''") + ", Message = " + exception1.Message.Replace("'", "''"));
            }
        }

        private void DataPosition_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            try
            {
                switch (e.PropertyName.ToUpper())
                {
                    case "RUNWAY":
                        // TODO: Aircraft enters runway or exit runway. Execute actions.
                        DtoDataPosition data = (DtoDataPosition)sender;
                        break;
                    default:
                        break;
                }
            }
            catch (Exception exception1)
            {
                LogHelper.Func_WriteEventInLogFile(DateTime.Now.ToLocalTime(), Enum_EventTypes.Error, "", string.Format("{0}.{1}()", MethodBase.GetCurrentMethod().DeclaringType.FullName, MethodBase.GetCurrentMethod().Name), "GeneralException",
                           "Source = " + exception1.Source.Replace("'", "''") + ", Message = " + exception1.Message.Replace("'", "''"));
            }
        }
    }
}
