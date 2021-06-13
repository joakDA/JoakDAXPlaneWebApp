using System;
using XPlaneUDPExchange.Helpers;
using XPlaneUDPExchange.Model.Data;

namespace XPlaneUDPExchange.Model.DTO
{
    public class DtoDataWeather : DtoData
    {
        #region PROPERTIES

        /// <summary>
        /// SLprs, in inches mercury.
        /// </summary>
        public double SeaLevelPressure { get; set; }

        /// <summary>
        /// SLtmp, in degrees Celsius.
        /// </summary>
        public double SeaLevelTemperature { get; set; }

        /// <summary>
        /// The wind speed around the aircraft, in knots.
        /// </summary>
        public double WindSpeed { get; set; }

        /// <summary>
        /// The wind direction, in degrees clockwise deviation from north. Wind blowing from north to south has direction 0.0, while wind blowing from west to east has direction 270.0.
        /// </summary>
        public double WindDirection { get; set; }

        /// <summary>
        /// Amount of local turbulence.
        /// </summary>
        public double Turbulence { get; set; }

        /// <summary>
        /// Amount of local precipitation.
        /// </summary>
        public double Precipitation { get; set; }

        #endregion

        public DtoDataWeather()
        {
            this.DataType = Enum_DataGroup.Weather;
        }

        public DtoDataWeather(DataWeather data)
        {
            this.DataType = Enum_DataGroup.Weather;
            this.SeaLevelPressure = Math.Round(data.SeaLevelPressure, 2);
            this.SeaLevelTemperature = Math.Round(data.SeaLevelTemperature, 2);
            this.WindSpeed = Math.Round(data.WindSpeed, 2);
            this.WindDirection = Math.Round(data.WindDirection, 2);
            this.Turbulence = Math.Round(data.Turbulence, 2);
            this.Precipitation = Math.Round(data.Precipitation, 2);
        }
    }
}
