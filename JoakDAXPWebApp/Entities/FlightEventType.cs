using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace JoakDAXPWebApp.Entities
{
    public class FlightEventType : BaseEntity
    {
        #region PROPERTIES

        /// <summary>
        /// Unique identifier of the event type.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name of the event.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// <c>true</c> if event type is enabled. <c>false</c> in other case.
        /// </summary>
        public bool Enabled { get; set; }

        [JsonIgnore]
        public IList<Flight> Flights { get; set; }

        #endregion
    }
}
