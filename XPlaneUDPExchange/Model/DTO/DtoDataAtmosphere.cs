using System;
using XPlaneUDPExchange.Helpers;
using XPlaneUDPExchange.Model.Data;

namespace XPlaneUDPExchange.Model.DTO
{
    public class DtoDataAtmosphere : DtoData
    {
        #region PROPERTIES

        /// <summary>
        /// AMprs, in inches mercury.
        /// </summary>
        public double AmbientPressure { get; set; }

        /// <summary>
        /// AMtmp, in degrees Celsius.
        /// </summary>
        public double AmbientTemperature { get; set; }

        #endregion

        public DtoDataAtmosphere()
        {
            this.DataType = Enum_DataGroup.AircraftPressures;
        }

        public DtoDataAtmosphere(DataAtmosphere data)
        {
            this.DataType = Enum_DataGroup.AircraftPressures;
            this.AmbientPressure = Math.Round(data.AmbientPressureHg, 2);
            this.AmbientTemperature = Math.Round(data.AmbientTemperatureDegC, 2);
        }
    }
}
