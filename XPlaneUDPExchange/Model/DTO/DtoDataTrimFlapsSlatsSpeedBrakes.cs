using System;
using XPlaneUDPExchange.Helpers;
using XPlaneUDPExchange.Model.Data;

namespace XPlaneUDPExchange.Model.DTO
{
    public class DtoDataTrimFlapsSlatsSpeedBrakes : DtoData
    {
        #region PROPERTIES

        /// <summary>
        /// Elevator trim.
        /// </summary>
        public double ElevatorTrim { get; set; }

        /// <summary>
        /// Aileron trim.
        /// </summary>
        public double AileronTrim { get; set; }

        /// <summary>
        /// Rudder trim.
        /// </summary>
        public double RuddlerTrim { get; set; }

        /// <summary>
        /// Flap position.
        /// </summary>
        public double FlapPosition { get; set; }

        /// <summary>
        /// Speedbrake position.
        /// </summary>
        public double SpeedBrakePosition { get; set; }

        #endregion

        public DtoDataTrimFlapsSlatsSpeedBrakes()
        {
            this.DataType = Enum_DataGroup.TrimFlapsSlatsSpeedBrakes;
        }

        public DtoDataTrimFlapsSlatsSpeedBrakes(DataTrimFlapsSlatsSpeedBrakes data)
        {
            this.DataType = Enum_DataGroup.TrimFlapsSlatsSpeedBrakes;
            this.ElevatorTrim = Math.Round(data.ElevatorTrim, 2);
            this.AileronTrim = Math.Round(data.AileronTrim, 2);
            this.RuddlerTrim = Math.Round(data.RuddlerTrim, 2);
            this.FlapPosition = Math.Round((data.FlapPosition * 100), 2); // Convert to porcentage.
            this.SpeedBrakePosition = Math.Round((data.SpeedBrakePosition * 100), 2); // Convert to porcentage.
        }
    }
}
