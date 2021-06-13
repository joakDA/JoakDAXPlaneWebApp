using System;
using XPlaneUDPExchange.Helpers;
using XPlaneUDPExchange.Model.Data;

namespace XPlaneUDPExchange.Model.DTO
{
    public class DtoDataAttitude : DtoData
    {
        #region PROPERTIES

        /// <summary>
        /// The aircraft’s pitch, measured in body-axis Euler angles.
        /// </summary>
        public double Pitch { get; set; }

        /// <summary>
        /// The aircraft’s roll, measured in body-axis Euler angles.
        /// </summary>
        public double Roll { get; set; }

        /// <summary>
        /// The aircraft’s true heading, measured in body-axis Euler angles.
        /// </summary>
        public double HeadingTrue { get; set; }

        #endregion

        public DtoDataAttitude()
        {
            this.DataType = Enum_DataGroup.PitchRollHeadings;
        }

        public DtoDataAttitude(DataAttitude data)
        {
            this.DataType = Enum_DataGroup.PitchRollHeadings;
            this.Pitch = Math.Round(data.Pitch, 2);
            this.Roll = Math.Round(data.Roll, 2);
            this.HeadingTrue = Math.Round(data.HeadingTrue, 2);
        }
    }
}
