using System;
using XPlaneUDPExchange.Helpers;
using XPlaneUDPExchange.Model.Data;

namespace XPlaneUDPExchange.Model.DTO
{
    public class DtoDataMachVvi : DtoData
    {
        #region PROPERTIES

        /// <summary>
        /// The aircraft’s current speed, as a ratio to Mach 1.
        /// </summary>
        public double Mach { get; set; }

        /// <summary>
        /// The aircraft’s current vertical velocity, in feet per minute. If everything is working as it should, this is normal to the local horizon under the aircraft.
        /// </summary>
        public double VerticalSpeed { get; set; }

        #endregion

        public DtoDataMachVvi()
        {
            this.DataType = Enum_DataGroup.MatchVVIGLoad;
        }

        public DtoDataMachVvi(DataMachVviGLoad data)
        {
            this.DataType = Enum_DataGroup.MatchVVIGLoad;
            this.Mach = Math.Round(data.Mach, 2);
            this.VerticalSpeed = Math.Round(data.VerticalSpeed, 2);
        }
    }
}
