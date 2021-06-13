using System;
using System.Collections.Generic;
using System.Text;
using XPlaneUDPExchange.Helpers;
using XPlaneUDPExchange.Model.Data;

namespace XPlaneUDPExchange.Model.DTO
{
    public class DtoDataTimes : DtoData
    {
        #region PROPERTIES

        /// <summary> (s)
        /// The time since the start of the “mission” (generally the time since the last time an aircraft or location was loaded).
        /// </summary>
        public TimeSpan MissionTime { get; set; }

        /// <summary>
        /// “Zulu” time (Greenwich Mean Time, or GMT) in the simulator, in decimal hours (e.g., 3.5 for 3:30 a.m.).
        /// </summary>
        public DateTime ZuluTime { get; set; }

        /// <summary>
        /// Local time in the simulator, in decimal hours.
        /// </summary>
        public DateTime LocalTime { get; set; }

        #endregion

        public DtoDataTimes()
        {
            this.DataType = Enum_DataGroup.Times;
        }

        public DtoDataTimes(DataTimes data)
        {
            this.DataType = Enum_DataGroup.Times;
            this.MissionTime = TimeSpan.FromSeconds(data.MissionTime);
            TimeSpan zuluTs = TimeSpan.FromHours(data.ZuluTime);
            DateTime dtNow = DateTime.UtcNow;
            this.ZuluTime = dtNow.AddTicks(zuluTs.Ticks);
            TimeSpan localTs = TimeSpan.FromHours(data.LocalTime);
            dtNow = DateTime.UtcNow;
            this.LocalTime = dtNow.AddTicks(localTs.Ticks);
        }
    }
}
