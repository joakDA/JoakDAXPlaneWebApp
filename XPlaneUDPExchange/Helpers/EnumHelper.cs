namespace XPlaneUDPExchange.Helpers
{
    /// <summary>
    /// Enum used to display type of log message.
    /// </summary>
    public enum Enum_EventTypes
    {
        /// <summary>
        /// Events to trace application.
        /// </summary>
        Debug = 0,
        /// <summary>
        /// To show information messages.
        /// </summary>
        Info = 1,
        /// <summary>
        /// Show error messages.
        /// </summary>
        Error = 2,
        /// <summary>
        /// Show warning messages.
        /// </summary>
        Warning = 3,
        /// <summary>
        /// Show notice messages.
        /// </summary>
        Notice = 4
    }
}