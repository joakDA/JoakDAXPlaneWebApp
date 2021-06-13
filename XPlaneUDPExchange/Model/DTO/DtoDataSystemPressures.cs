using System;
using XPlaneUDPExchange.Helpers;
using XPlaneUDPExchange.Model.Data;

namespace XPlaneUDPExchange.Model.DTO
{
    public class DtoDataSystemPressures : DtoData
    {
        #region PROPERTIES

        /// <summary>
        /// Barometric pressure, in inches mercury.
        /// </summary>
        public double BarometricPressure { get; set; }

        #endregion

        public DtoDataSystemPressures()
        {
            this.DataType = Enum_DataGroup.SystemPressures;
        }

        public DtoDataSystemPressures(DataSystemPressures data)
        {
            this.DataType = Enum_DataGroup.SystemPressures;
            this.BarometricPressure = Math.Round(data.BarometricPressure, 2);
        }
    }
}
