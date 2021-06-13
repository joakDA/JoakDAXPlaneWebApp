using XPlaneUDPExchange.Helpers;
namespace XPlaneUDPExchange.Model.Data
{
    public class DataFrameRate : XPlaneData
    {
        #region PROPERTIES

        /// <summary>
        /// The actual frame rate being displayed by X-Plane. Unless your computer is bogged down, this will be the same as f-sim.
        /// </summary>
        public float FrameRate { get; set; }

        /// <summary>
        /// The frame rate the simulator is “pretending” to have in order to keep the flight model from failing.
        /// </summary>
        public float FrameRateSim { get; set; }

        /// <summary>
        /// The time required to render one frame, in seconds.
        /// </summary>
        public float FrameTime { get; set; }

        /// <summary>
        /// CPU time.
        /// </summary>
        public float CpuTime { get; set; }

        /// <summary>
        /// GPU time.
        /// </summary>
        public float GpuTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public float GroundRatio { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public float FlitRatio { get; set; }

        #endregion

        public DataFrameRate()
        {
            this.DataGroup = Enum_DataGroup.FrameRate;
        }

        public override string ToString()
        {
            return string.Format("Frame Rate: {0}; Frame Ratio Sim: {1}; Frame Time: {2}; CPU Time: {3}; GPU Time: {4}; Ground Ratio: {5}; Flight Ratio: {6}",
                FrameRate.ToString(), FrameRateSim.ToString(), FrameTime.ToString(), CpuTime.ToString(), GpuTime.ToString(), GroundRatio.ToString(), FlitRatio.ToString()); 
        }
    }
}
