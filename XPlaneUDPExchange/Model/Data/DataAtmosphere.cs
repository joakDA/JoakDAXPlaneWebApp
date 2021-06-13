using XPlaneUDPExchange.Helpers;
namespace XPlaneUDPExchange.Model.Data
{
    public class DataAtmosphere : XPlaneData
    {
        #region PROPERTIES
        /// <summary>
        /// AMprs, in inches mercury.
        /// </summary>
        public float AmbientPressureHg { get; set; }

        /// <summary>
        /// AMtmp, in degrees Celsius.
        /// </summary>
        public float AmbientTemperatureDegC { get; set; }

        /// <summary>
        /// LEtmp, in degrees Celsius.
        /// </summary>
        public float LeadingEdgeTemperatureDegC { get; set; }

        /// <summary>
        /// The aircraft’s density ratio.
        /// </summary>
        public float AirDensityRatio { get; set; }

        /// <summary>
        /// A, in knots true airspeed.
        /// </summary>
        public float Akts { get; set; }

        /// <summary>
        /// Q, dynamic pressure or velocity pressure, in pounds per square foot.
        /// </summary>
        public float QPsf { get; set; }

        /// <summary>
        /// Gravitational force, in feet per second squared.
        /// </summary>
        public float GravityFtSecSqrd { get; set; }

        #endregion

        public DataAtmosphere()
        {
            this.DataGroup = Enum_DataGroup.AircraftPressures;
        }

        public override string ToString()
        {
            return string.Format("Ambient Pressure: {0} mm Hg; Ambient Temperature: {1} ºC; Leading Edge Temperature: {2} ºC; Air Density Ratio (sigma): {3}; A: {4} kts; Pounds per Square Feet: {5} Q; Gravity: {6} (ft per second^2).", 
                this.AmbientPressureHg.ToString(), this.AmbientTemperatureDegC.ToString(), this.LeadingEdgeTemperatureDegC.ToString(), this.AirDensityRatio.ToString(), this.Akts.ToString(), this.QPsf.ToString(), this.GravityFtSecSqrd.ToString());
        }
    }
}
