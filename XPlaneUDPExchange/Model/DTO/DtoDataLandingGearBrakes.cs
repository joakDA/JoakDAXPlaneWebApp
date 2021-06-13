using System;
using XPlaneUDPExchange.Helpers;
using XPlaneUDPExchange.Model.Data;

namespace XPlaneUDPExchange.Model.DTO
{
    public class DtoDataLandingGearBrakes : DtoData
    {
        #region PROPERTIES

        /// <summary>
        /// Gear extended status. (false --> not extended, true --> extended).
        /// </summary>
        public bool GearExtended { get; set; }

        #endregion

        public DtoDataLandingGearBrakes()
        {
            this.DataType = Enum_DataGroup.LandingGearBrakes;
        }

        public DtoDataLandingGearBrakes(DataLandingGearBrakes data)
        {
            this.DataType = Enum_DataGroup.LandingGearBrakes;
            this.GearExtended = Convert.ToBoolean(data.GearExtended);
        }
    }
}
