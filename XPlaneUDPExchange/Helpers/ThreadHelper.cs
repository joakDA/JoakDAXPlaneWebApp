using System.Threading;

namespace XPlaneUDPExchange.Helpers
{
    public class ThreadHelper
    {
        #region PUBLIC_STATIC_METHODS

        /// <summary>
        /// Create and start a thread in background or not depending parameters.
        /// </summary>
        /// <param name="objectType">Job to execute in thread.</param>
        /// <param name="sName">Name of thread.</param>
        /// <param name="bBackground">True if run in background. False in other case.</param>
        public static void Func_Create_Start_Thread(ThreadStart objectType, string sName, bool bBackground)
        {
            Thread threadResult = new Thread(objectType);
            threadResult.Name = sName;
            threadResult.IsBackground = bBackground;
            threadResult.Start();
        }

        /// <summary>
        /// Create and start a thread in background or not depending parameters, receiving a parameter.
        /// </summary>
        /// <param name="objectType">Job to execute in thread.</param>
        /// <param name="sName">Name of thread.</param>
        /// <param name="bBackground">True if run in background. False in other case.</param>
        /// <param name="parameter1">Parameter to send to the thread.</param>
        public static void Func_Create_Start_Thread(ThreadStart objectType, string sName, bool bBackground, dynamic parameter1)
        {
            Thread threadResult = new Thread(objectType);
            threadResult.Name = sName;
            threadResult.IsBackground = bBackground;
            threadResult.Start(parameter1);
        }

        #endregion
    }
}
