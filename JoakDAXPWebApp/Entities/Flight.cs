using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace JoakDAXPWebApp.Entities
{
    public class Flight : BaseEntity
    {
        #region PROPERTIES

        /// <summary>
        /// Unique identifier of the flight on the system.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Event Datetime for landing or takeoff. On UTC timezone.
        /// </summary>
        public DateTime EventDateTime { get; set; }

        /// <summary>
        /// Event type. This is a foreign entity.
        /// </summary>
        public FlightEventType FlightEventType { get; set; }

        /// <summary>
        /// A string containing all available data about the position where the flight event was triggered.
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// Current latitude when the event was triggered.
        /// </summary>
        public double Latitude { get; set; }

        /// <summary>
        /// Current latitude when the event was triggered.
        /// </summary>
        public double Longitude { get; set; }

        /// <summary>
        /// Distance (in meters) from ideal landing lane.
        /// </summary>
        public double? DistanceFromIdeal { get; set; }

        /// <summary>
        /// Glideslope score got. (Landing only).
        /// </summary>
        public double? GlideslopeScore { get; set; }

        /// <summary>
        /// Vertical speed (Landing only). Units: ft/min.
        /// </summary>
        public double? VerticalSpeed { get; set; }

        /// <summary>
        /// Max force executed on the plane (lb).
        /// </summary>
        public double? MaxForce { get; set; }

        /// <summary>
        /// Current pitch value.
        /// </summary>
        public double? Pitch { get; set; }

        #endregion
    }
}
