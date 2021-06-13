using XPlaneUDPExchange.Helpers;
namespace XPlaneUDPExchange.Model.Data
{
    public class DataMachVviGLoad : XPlaneData
    {
        #region PROPERTIES

        /// <summary>
        /// The aircraft’s current speed, as a ratio to Mach 1.
        /// </summary>
        public float Mach { get; set; }

        /// <summary>
        /// The aircraft’s current vertical velocity, in feet per minute. If everything is working as it should, this is normal to the local horizon under the aircraft.
        /// </summary>
        public float VerticalSpeed { get; set; }

        /// <summary>
        /// The g-load across the aircraft, relative to the aircraft body-axis, if all is working as it should.
        /// </summary>
        public float GLoadNormal { get; set; }

        /// <summary>
        /// The axial g-load on the aircraft.
        /// </summary>
        public float GLoadAxial { get; set; }

        /// <summary>
        /// The side g-load on the aircraft.
        /// </summary>
        public float GLoadSide { get; set; }

        #endregion

        public DataMachVviGLoad()
        {
            this.DataGroup = Enum_DataGroup.MatchVVIGLoad;
        }

        public override string ToString()
        {
            return string.Format("Mach Speed: {0}; Vertical speed: {1} feet/min; Gload (normal): {2}; Gload (axial): {3}; Gload (side): {4}.", 
                Mach.ToString(), VerticalSpeed.ToString(), GLoadNormal.ToString(), GLoadAxial.ToString(), GLoadSide.ToString());
        }
    }
}
