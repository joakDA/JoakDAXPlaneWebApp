using XPlaneUDPExchange.Helpers;
namespace XPlaneUDPExchange.Model.Data
{
    public class DataTrimFlapsSlatsSpeedBrakes : XPlaneData
    {
        #region PROPERTIES

        /// <summary>
        /// Elevator trim.
        /// </summary>
        public float ElevatorTrim { get; set; }

        /// <summary>
        /// Aileron trim.
        /// </summary>
        public float AileronTrim { get; set; }

        /// <summary>
        /// Rudder trim.
        /// </summary>
        public float RuddlerTrim { get; set; }

        /// <summary>
        /// Useless, please ignore.
        /// </summary>
        public float Flap { get; set; }

        /// <summary>
        /// Flap position.
        /// </summary>
        public float FlapPosition { get; set; }

        /// <summary>
        /// Slat position.
        /// </summary>
        public float SlatPosition { get; set; }

        /// <summary>
        /// Useless, please ignore.
        /// </summary>
        public float SpeedBrake { get; set; }

        /// <summary>
        /// Speedbrake position.
        /// </summary>
        public float SpeedBrakePosition { get; set; }

        #endregion

        public DataTrimFlapsSlatsSpeedBrakes()
        {
            this.DataGroup = Enum_DataGroup.TrimFlapsSlatsSpeedBrakes;
        }

        public override string ToString()
        {
            return string.Format("Elevator trim: {0}; Aileron trim: {1}; Rudder trim: {2}; Flap: {3}; Flap position: {4}; Slat position: {5}; Speed brake: {6}; Speed brake position: {7}.",
                ElevatorTrim.ToString(), AileronTrim.ToString(), RuddlerTrim.ToString(), Flap.ToString(), FlapPosition.ToString(), SlatPosition.ToString(), SpeedBrake.ToString(), SpeedBrakePosition.ToString());
        }
    }
}
