using XPlaneUDPExchange.Helpers;
namespace XPlaneUDPExchange.Model.Data
{
    public class DataSystemPressures : XPlaneData
    {
        #region PROPERTIES

        /// <summary>
        /// Barometric pressure, in inches mercury.
        /// </summary>
        public float BarometricPressure { get; set; }

        /// <summary>
        /// Useless.
        /// </summary>
        public float Edens { get; set; }

        /// <summary>
        /// Useless.
        /// </summary>
        public float Vacum { get; set; }

        /// <summary>
        /// Useless.
        /// </summary>
        public float Elec { get; set; }

        /// <summary>
        /// Pressure in the attitude heading reference system (AHRS), as a ratio to normal.
        /// </summary>
        public float AHRS { get; set; }

        #endregion

        public DataSystemPressures()
        {
            this.DataGroup = Enum_DataGroup.SystemPressures;
        }

        public override string ToString()
        {
            return string.Format("Barometric Pressure: {0} inHg; Edens: {1}; Vacum: {2}; Elec: {3}; AHRS: {4}.",
                BarometricPressure.ToString(), Edens.ToString(), Vacum.ToString(), Elec.ToString(), AHRS.ToString());
        }
    }
}
