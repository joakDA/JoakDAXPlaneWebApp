using XPlaneUDPExchange.Helpers;
namespace XPlaneUDPExchange.Model.Data
{
    public class DataPosition : XPlaneData
    {
        #region PROPERTIES

        /// <summary>
        /// The aircraft’s latitudinal location, in degrees.
        /// </summary>
        public float Latitude { get; set; }

        /// <summary>
        /// The aircraft’s longitudinal location, in degrees.
        /// </summary>
        public float Longitude { get; set; }

        /// <summary>
        /// The aircraft’s altitude, in feet above mean sea level.
        /// </summary>
        public float AltitudeSeaLevel { get; set; }

        /// <summary>
        /// The aircraft’s altitude, in feet above ground level.
        /// </summary>
        public float AltitudeGroundLevel { get; set; }

        /// <summary>
        /// 0 --> aircraft is not over runway. 1 --> aircraft over runway.
        /// </summary>
        public float Runway { get; set; }

        /// <summary>
        /// Altitude (indicated).
        /// </summary>
        public float AltitudeIndicated { get; set; }

        /// <summary>
        /// Latitude normalized.
        /// </summary>
        public float LatitudeNormalized { get; set; }

        /// <summary>
        /// Longitude normalized.
        /// </summary>
        public float LongitudeNormalized { get; set; }

        #endregion

        public DataPosition()
        {
            this.DataGroup = Enum_DataGroup.LatitudeLongitudeAltitude;
        }

        public override string ToString()
        {
            return string.Format("Latitude: {0}; Longitude: {1}; Altitude (Sea Level): {2} ft; Altitude (Ground Level): {3}; Runway: {4}; Altitude (indicated): {5}; Latitude (normalized): {6}; Longitude (normalized): {7}.",
                Latitude.ToString(), Longitude.ToString(), AltitudeSeaLevel.ToString(), AltitudeGroundLevel.ToString(), Runway.ToString() ,AltitudeIndicated.ToString(), LatitudeNormalized.ToString(), LongitudeNormalized.ToString());
        }
    }
}
