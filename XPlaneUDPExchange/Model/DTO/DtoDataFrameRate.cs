using System;
using XPlaneUDPExchange.Helpers;
using XPlaneUDPExchange.Model.Data;

namespace XPlaneUDPExchange.Model.DTO
{
    public class DtoDataFrameRate : DtoData
    {
        #region PROPERTIES

        /// <summary>
        /// The actual frame rate being displayed by X-Plane. Unless your computer is bogged down, this will be the same as f-sim.
        /// </summary>
        public double FrameRate { get; set; }

        /// <summary>
        /// The time required to render one frame, in seconds.
        /// </summary>
        public double FrameTime { get; set; }

        /// <summary>
        /// CPU time.
        /// </summary>
        public double CpuTime { get; set; }

        /// <summary>
        /// GPU time.
        /// </summary>
        public double GpuTime { get; set; }

        #endregion

        public DtoDataFrameRate()
        {
            this.DataType = Enum_DataGroup.FrameRate;
        }

        public DtoDataFrameRate(DataFrameRate data)
        {
            this.DataType = Enum_DataGroup.FrameRate;
            this.FrameRate = Math.Round(data.FrameRate, 2);
            this.FrameTime = Math.Round(data.FrameTime, 2);
            this.CpuTime = Math.Round(data.CpuTime, 2);
            this.GpuTime = Math.Round(data.GpuTime, 2);
        }
    }
}
