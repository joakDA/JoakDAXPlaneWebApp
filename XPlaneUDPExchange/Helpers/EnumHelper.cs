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

    public enum Enum_DataGroup
    {
        Unknown = -1,
        FrameRate = 0,
        Times = 1,
        Speeds = 3,
        MatchVVIGLoad = 4,
        Weather = 5,
        AircraftPressures = 6,
        SystemPressures = 7,
        TrimFlapsSlatsSpeedBrakes = 13,
        LandingGearBrakes = 14,
        PitchRollHeadings = 17,
        LatitudeLongitudeAltitude = 20,
        LocationVelocityDistanceTraveled = 21,
        AllLatitude = 22,
        AllLongitude = 23,
        AllAltitude = 24,
        FuelWeights = 62,
        PayloadWeights = 63,
        LandingGearVerticalForce = 66,
        LandingGearDeployment = 67,
        RadioCOMFrequency = 96,
        RadioNAVFrequency = 97,
        RadioNAVOBS = 98,
        ADFStatus = 101,
        DMEStatus = 102,
        GpsStatus = 103,
        TransponderStatus = 104,
        ElectricalSwitches = 106,
        EFISSwitches = 107,
        AutopilotFDHUDSwitches = 108,
        GeneralAnunciators = 113,
        EngineAnnunciators = 114,
        AutopilotArmed = 116,
        AutopilotModes = 117,
        WarningStatus = 127,
        FlightPlanLegs = 128,
        ClimbStatistics = 132,
        CruiseStatistics = 133
    }
}