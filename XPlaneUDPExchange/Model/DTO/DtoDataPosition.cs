using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using XPlaneUDPExchange.Helpers;
using XPlaneUDPExchange.Model.Data;

namespace XPlaneUDPExchange.Model.DTO
{
    public class DtoDataPosition : DtoData, INotifyPropertyChanged
    {
        #region PROPERTIES

        public event PropertyChangedEventHandler PropertyChanged;

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
        public double AltitudeSeaLevel { get; set; }

        /// <summary>
        /// The aircraft’s altitude, in feet above ground level.
        /// </summary>
        public double AltitudeGroundLevel { get; set; }

        private bool _runway;

        /// <summary>
        /// false --> aircraft is not over runway. true --> aircraft over runway.
        /// </summary>
        public bool Runway { 
            get 
            {
                return this._runway;
            } 
            set 
            {
                if (value != this._runway)
                {
                    this._runway = value;
                    NotifyPropertyChanged("Runway");
                }
            } 
        }

        /// <summary>
        /// Altitude (indicated).
        /// </summary>
        public double AltitudeIndicated { get; set; }

        #endregion

        public DtoDataPosition()
        {
            this.DataType = Enum_DataGroup.LatitudeLongitudeAltitude;
        }

        public DtoDataPosition(DataPosition data)
        {
            this.DataType = Enum_DataGroup.LatitudeLongitudeAltitude;
            this.Latitude = data.Latitude;
            this.Longitude = data.Longitude;
            this.AltitudeSeaLevel = Math.Round(data.AltitudeSeaLevel, 2);
            this.AltitudeGroundLevel = Math.Round(data.AltitudeGroundLevel, 2);
            this.Runway = Convert.ToBoolean(data.Runway);
            this.AltitudeIndicated = Math.Round(data.AltitudeIndicated, 2);
        }

        // This method is called by the Set accessor of each property.
        // The CallerMemberName attribute that is applied to the optional propertyName
        // parameter causes the property name of the caller to be substituted as an argument.
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
