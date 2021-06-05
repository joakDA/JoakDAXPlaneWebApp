using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading;
using XPlaneUDPExchange.Model;

namespace XPlaneUDPExchange.Helpers
{
    class LogHelper
    {
        #region PUBLIC_MEMBER_PROPERTIES

        /// <summary>
        /// Event handler.
        /// </summary>
        public static event EventHandler<CustomEventArgs> DataReceived = null;

        #endregion

        #region PRIVATE_MEMBER_PROPERTIES

        /// <summary>
        /// Mutex to avoid that two threads write in file at the same time.
        /// </summary>
        private static Mutex writeEventInLogFileMutex = new Mutex();

        #endregion

        #region PUBLIC_STATIC_METHODS

        /// <summary>
        /// Write in log the events occurred in application.
        /// </summary>
        /// <param name="event_UT">Datetime of event.</param>
        /// <param name="eventType">Type of event.</param>
        /// <param name="username">User name.</param>
        /// <param name="function">Function when the event occurs.</param>
        /// <param name="eventLabel">Label of event. (Example: Data sent:).</param>
        /// <param name="data">Message with detailed information about the event.</param>
        internal static void Func_WriteEventInLogFile(DateTime event_UT, Enum_EventTypes eventType, string username, string function, string eventLabel, string data)
        {
            string s1;

            try
            {
                //Blocks the current thread until Mutex has been released
                writeEventInLogFileMutex.WaitOne();

                s1 = string.Format("{0};{1};{2};{3};{4};{5}", event_UT.ToLocalTime().ToString("dd/MM/yyyy HH:mm:ss.fff"), eventType,
                    username, function, eventLabel, data);

                if ((eventType == Enum_EventTypes.Debug && Config.debugMode) || eventType == Enum_EventTypes.Info || eventType == Enum_EventTypes.Error)
                {
                    Func_WriteToLog(s1, event_UT);
                    //s1 += "\r\n";
                    OnDataReceived(null, new CustomEventArgs { message = s1, eventType = eventType });
                }
                //Unlock the current thread
                writeEventInLogFileMutex.ReleaseMutex();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                writeEventInLogFileMutex.ReleaseMutex();
            }
        }

        #endregion

        #region PRIVATE_STATIC_METHODS

        /// <summary>
        /// Write a string in a log file.
        /// </summary>
        /// <param name="s1">String with the data to write in file.</param>
        /// <param name="event_UT">Datatime of the event.</param>
        private static void Func_WriteToLog(string s1, DateTime event_UT)
        {
            StreamWriter StreamWriter1;
            string sHeader = "";
            string sPath = Path.Combine(Config.logPath, string.Format("{0}-{1}-{2}.csv", event_UT.ToLocalTime().ToString("yyyy_MM_dd"), Assembly.GetExecutingAssembly().GetName().Name, "Log"));
            if (!File.Exists(sPath))
            {
                sHeader = string.Format("{0};{1};{2};{3};{4};{5}", "Date and time", "Event type", "User", "Function", "Event", "Data");
            }
            StreamWriter1 = File.AppendText(sPath);
            if (!string.IsNullOrEmpty(sHeader))
            {
                StreamWriter1.Write(sHeader);
            }
            StreamWriter1.WriteLine(s1);
            StreamWriter1.Flush();
            StreamWriter1.Close();
        }

        /// <summary>
        /// Method to set messages to event handlers.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">Event data with a string with the data to return to event.</param>
        private static void OnDataReceived(object sender, CustomEventArgs e)
        {
            if (DataReceived != null)
            {
                DataReceived(null, new CustomEventArgs { message = e.message, eventType = e.eventType });
            }
        }

        #endregion
    }
}
