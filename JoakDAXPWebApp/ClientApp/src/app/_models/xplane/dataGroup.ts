export enum DataGroup {
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