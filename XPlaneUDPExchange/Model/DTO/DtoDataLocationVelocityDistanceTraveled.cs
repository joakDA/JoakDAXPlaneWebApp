using System;
using XPlaneUDPExchange.Helpers;
using XPlaneUDPExchange.Model.Data;

namespace XPlaneUDPExchange.Model.DTO
{
    public class DtoDataLocationVelocityDistanceTraveled : DtoData
    {
        #region PROPERTIES

        /// <summary>
        /// Distance traveled (in nautical miles).
        /// </summary>
        public double DistanceNm { get; set; }

        #endregion

        public DtoDataLocationVelocityDistanceTraveled()
        {
            this.DataType = Enum_DataGroup.LocationVelocityDistanceTraveled;
        }

        public DtoDataLocationVelocityDistanceTraveled(DataLocationVelocityDistanceTraveled data)
        {
            this.DataType = Enum_DataGroup.LocationVelocityDistanceTraveled;
            this.DistanceNm = Math.Round(data.DistanceNm, 2);
        }
    }
}
