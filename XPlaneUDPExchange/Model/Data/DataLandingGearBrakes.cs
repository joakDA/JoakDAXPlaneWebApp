using XPlaneUDPExchange.Helpers;
namespace XPlaneUDPExchange.Model.Data
{
    public class DataLandingGearBrakes : XPlaneData
    {
        #region PROPERTIES

        /// <summary>
        /// Gear extended status. (0, 1).
        /// </summary>
        public int GearExtended { get; set; }

        /// <summary>
        /// Useless, please ignore.
        /// </summary>
        public float WBrake {get; set;}

        /// <summary>
        /// Left toe brake depression.
        /// </summary>
        public float LeftBrake { get; set; }

        /// <summary>
        /// Right toe brake depression.
        /// </summary>
        public float RightBrake { get; set; }

        #endregion

        public DataLandingGearBrakes()
        {
            this.DataGroup = Enum_DataGroup.LandingGearBrakes;
        }

        public override string ToString()
        {
            return string.Format("Gear extended status: {0}; W brake: {1}; Left brake: {2}; Right brake: {3}",
                GearExtended.ToString(), WBrake.ToString(), LeftBrake.ToString(), RightBrake.ToString());
        }
    }
}
