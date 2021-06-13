using System;
using XPlaneUDPExchange.Helpers;
using XPlaneUDPExchange.Model.Data;

namespace XPlaneUDPExchange.Model.DTO
{
    public class DtoDataSpeed : DtoData
    {
        #region PROPERTIES

        /// <summary>
        /// The craft’s indicated airspeed, in knots indicated airspeed.
        /// </summary>
        public double VIndKts { get; set; }

        #endregion

        public DtoDataSpeed()
        {
            this.DataType = Enum_DataGroup.Speeds;
        }

        public DtoDataSpeed(DataSpeed data)
        {
            this.DataType = Enum_DataGroup.Speeds;
            this.VIndKts = Math.Round(data.VIndKts, 2);
        }
    }
}
