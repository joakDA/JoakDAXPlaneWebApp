using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XPlaneUDPExchange.Model.DTO;

namespace JoakDAXPWebApp.Models
{
    public class XPlaneDataModel
    {
        #region PROPERTIES

        public DtoDataAtmosphere DataAtmosphere { get; set; }

        public DtoDataAttitude DataAttitude { get; set; }

        public DtoDataFrameRate DataFrameRate { get; set; }

        public DtoDataLandingGearBrakes DataLandingGearBrakes { get; set; }

        public DtoDataLocationVelocityDistanceTraveled DataLocationVelocityDistanceTraveled { get; set; }

        public DtoDataMachVvi DataMachVvi { get; set; }

        public DtoDataPosition DataPosition { get; set; }

        public DtoDataSpeed DataSpeed { get; set; }

        public DtoDataSystemPressures DataSystemPressures { get; set; }

        public DtoDataTimes DataTimes { get; set; }

        public DtoDataTrimFlapsSlatsSpeedBrakes DataTrimFlapsSlatsSpeedBrakes { get; set; }

        public DtoDataWeather DataWeather { get; set; }

        #endregion

        public XPlaneDataModel()
        {
            DataAtmosphere = new DtoDataAtmosphere();
            DataAttitude = new DtoDataAttitude();
            DataFrameRate = new DtoDataFrameRate();
            DataLandingGearBrakes = new DtoDataLandingGearBrakes();
            DataLocationVelocityDistanceTraveled = new DtoDataLocationVelocityDistanceTraveled();
            DataMachVvi = new DtoDataMachVvi();
            DataPosition = new DtoDataPosition();
            DataSpeed = new DtoDataSpeed();
            DataSystemPressures = new DtoDataSystemPressures();
            DataTimes = new DtoDataTimes();
            DataTrimFlapsSlatsSpeedBrakes = new DtoDataTrimFlapsSlatsSpeedBrakes();
            DataWeather = new DtoDataWeather();
        }
    }
}
