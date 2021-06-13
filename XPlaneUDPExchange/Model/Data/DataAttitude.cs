using XPlaneUDPExchange.Helpers;
namespace XPlaneUDPExchange.Model.Data
{
    public class DataAttitude : XPlaneData
    {
        #region PROPERTIES

        /// <summary>
        /// The aircraft’s pitch, measured in body-axis Euler angles.
        /// </summary>
        public float Pitch { get; set; }

        /// <summary>
        /// The aircraft’s roll, measured in body-axis Euler angles.
        /// </summary>
        public float Roll { get; set; }

        /// <summary>
        /// The aircraft’s true heading, measured in body-axis Euler angles.
        /// </summary>
        public float HeadingTrue { get; set; }

        /// <summary>
        /// The aircraft’s magnetic heading, in degrees.
        /// </summary>
        public float HeadingMagnetic { get; set; }

        #endregion

        public DataAttitude()
        {
            this.DataGroup = Enum_DataGroup.PitchRollHeadings;
        }

        public override string ToString()
        {
            return string.Format("Pitch: {0} degrees; Roll: {1} degrees; Heading (true): {2} degrees; Heading (magnetic): {3};.",
                Pitch.ToString(), Roll.ToString(), HeadingTrue.ToString(), HeadingMagnetic.ToString());
        }
    }
}
