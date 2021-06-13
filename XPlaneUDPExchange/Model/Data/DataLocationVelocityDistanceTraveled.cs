using XPlaneUDPExchange.Helpers;
namespace XPlaneUDPExchange.Model.Data
{
    public class DataLocationVelocityDistanceTraveled : XPlaneData
    {
        #region PROPERTIES

        /// <summary>
        /// Relative to the inertial axes.
        /// </summary>
        public float X { get; set; }

        /// <summary>
        /// Relative to the inertial axes.
        /// </summary>
        public float Y { get; set; }

        /// <summary>
        /// Relative to the inertial axes.
        /// </summary>
        public float Z { get; set; }

        /// <summary>
        /// Relative to the inertial axes.
        /// </summary>
        public float vX { get; set; }

        /// <summary>
        /// Relative to the inertial axes.
        /// </summary>
        public float vY { get; set; }

        /// <summary>
        /// Relative to the inertial axes.
        /// </summary>
        public float vZ { get; set; }

        /// <summary>
        /// Distance traveled (in feets).
        /// </summary>
        public float DistanceFeet { get; set; }

        /// <summary>
        /// Distance traveled (in nautical miles).
        /// </summary>
        public float DistanceNm { get; set; }

        #endregion

        public DataLocationVelocityDistanceTraveled()
        {
            this.DataGroup = Enum_DataGroup.LocationVelocityDistanceTraveled;
        }

        public override string ToString()
        {
            return string.Format("X: {0} m; Y: {1} m; Z: {2} m,; vX: {3} m/s; vY: {4} m/s; vZ: {5} m/s; Distance: {6} ft; Distance: {7} nm.",
                X.ToString(), Y.ToString(), Z.ToString(), vX.ToString(), vY.ToString(), vZ.ToString(), DistanceFeet.ToString(), DistanceNm.ToString());
        }
    }
}
