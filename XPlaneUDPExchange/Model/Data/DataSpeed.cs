using XPlaneUDPExchange.Helpers;
namespace XPlaneUDPExchange.Model.Data
{
    public class DataSpeed : XPlaneData
    {
        #region PROPERTIES

        /// <summary>
        /// The craft’s indicated airspeed, in knots indicated airspeed.
        /// </summary>
        public float VIndKts { get; set; }

        /// <summary>
        /// The craft’s indicated airspeed, in knots equivalent airspeed (the calibrated airspeed corrected for adiabatic compressible flow at the craft’s current altitude).
        /// </summary>
        public float VIndKeas { get; set; }

        /// <summary>
        /// The craft’s true airspeed (the speed of the craft relative to undisturbed air), in knots true airspeed.
        /// </summary>
        public float VTrueKts { get; set; }

        /// <summary>
        /// True airspeed, in knots true ground speed.
        /// </summary>
        public float VTrueKtgs { get; set; }

        /// <summary>
        /// The craft’s indicated airspeed, in miles per hour.
        /// </summary>
        public float VIndMph { get; set; }

        /// <summary>
        /// The craft’s true airspeed, in miles per hour airspeed.
        /// </summary>
        public float VTrueMph { get; set; }

        /// <summary>
        /// The craft’s true airspeed, in miles per hour ground speed.
        /// </summary>
        public float VTrueMphgs { get; set; }

        #endregion

        public DataSpeed()
        {
            this.DataGroup = Enum_DataGroup.Speeds;
        }

        public override string ToString()
        {
            return string.Format("Indicated airspeed: {0} kts; Indicated airspeed: {1} keas; True airspeed: {2} kts; True airspeed: {3} ktgs; Indicated airspeed: {4} mph; True airspeed: {5} mph; True airspeed: {6} mphgs.",
                VIndKts.ToString(), VIndKeas.ToString(), VTrueKts.ToString(), VTrueKtgs.ToString(), VIndMph.ToString(), VTrueMph.ToString(), VTrueMphgs.ToString());
        }
    }
}
