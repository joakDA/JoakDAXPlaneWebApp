using XPlaneUDPExchange.Helpers;
namespace XPlaneUDPExchange.Model.Data
{
    public class DataTimes : XPlaneData
    {
        #region PROPERTIES

        /// <summary>
        /// The number of seconds, in the real world, elapsed since the simulator was launched.
        /// </summary>
        public float RealTime { get; set; }

        /// <summary>
        /// The number of seconds elapsed since the simulator was launched minus time spent in a loading screen.
        /// </summary>
        public float TotalTime { get; set; }

        /// <summary> (s)
        /// The time since the start of the “mission” (generally the time since the last time an aircraft or location was loaded).
        /// </summary>
        public float MissionTime { get; set; }

        /// <summary>
        /// The time elapsed on a timer for general use.
        /// </summary>
        public float TimerTime { get; set; }

        /// <summary>
        /// “Zulu” time (Greenwich Mean Time, or GMT) in the simulator, in decimal hours (e.g., 3.5 for 3:30 a.m.).
        /// </summary>
        public float ZuluTime { get; set; }

        /// <summary>
        /// Local time in the simulator, in decimal hours.
        /// </summary>
        public float LocalTime { get; set; }

        /// <summary>
        /// The aircraft’s Hobbs time (a measurement of how long the aircraft’s systems have been run).
        /// </summary>
        public float HobbsTime { get; set; }

        #endregion

        public DataTimes()
        {
            this.DataGroup = Enum_DataGroup.Times;
        }

        public override string ToString()
        {
            return string.Format("Real Time: {0}; Total Time: {1}; Mission Time: {2}; Timer Time: {3}; Zulu Time: {4}; Local Time: {5}; Hobbs Time: {6}.",
                RealTime.ToString(), TotalTime.ToString(), MissionTime.ToString(), TimerTime.ToString(), ZuluTime.ToString(), LocalTime.ToString(), HobbsTime.ToString());
        }
    }
}
