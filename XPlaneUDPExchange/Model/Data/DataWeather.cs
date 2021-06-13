using XPlaneUDPExchange.Helpers;
namespace XPlaneUDPExchange.Model.Data
{
    public class DataWeather : XPlaneData
    {
        #region PROPERTIES

        /// <summary>
        /// SLprs, in inches mercury.
        /// </summary>
        public float SeaLevelPressure { get; set; }

        /// <summary>
        /// SLtmp, in degrees Celsius.
        /// </summary>
        public float SeaLevelTemperature { get; set; }

        /// <summary>
        /// The wind speed around the aircraft, in knots.
        /// </summary>
        public float WindSpeed { get; set; }

        /// <summary>
        /// The wind direction, in degrees clockwise deviation from north. Wind blowing from north to south has direction 0.0, while wind blowing from west to east has direction 270.0.
        /// </summary>
        public float WindDirection { get; set; }

        /// <summary>
        /// Amount of local turbulence.
        /// </summary>
        public float Turbulence { get; set; }

        /// <summary>
        /// Amount of local precipitation.
        /// </summary>
        public float Precipitation { get; set; }

        /// <summary>
        /// Amount of local hail.
        /// </summary>
        public float Hail { get; set; }

        #endregion

        public DataWeather()
        {
            this.DataGroup = Enum_DataGroup.Weather;
        }

        public override string ToString()
        {
            return string.Format("Pressure: {0} mm/Hg; Temperature: {1} ºC; Wind Speed: {2} kts; Wind Direction: {3}; Turbulence: {4}; Precipitation: {5}; Hail: {6}.",
                SeaLevelPressure.ToString(), SeaLevelTemperature.ToString(), WindSpeed.ToString(), WindDirection.ToString(), Turbulence.ToString(), Precipitation.ToString(), Hail.ToString());
        }
    }
}
