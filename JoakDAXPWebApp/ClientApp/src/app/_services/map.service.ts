import {Injectable} from '@angular/core';
import {GpsPosition} from '../_models/gpsPosition';
import {environment} from '../../environments/environment';
import * as L from 'leaflet';
import 'leaflet-extra-markers';

@Injectable({
  providedIn: 'root'
})
export class MapService {
  aircraftIcon: any;
  currentFlightPositions: GpsPosition[] = [];
  aircraftMarker: L.Marker;
  polyline: L.Polyline; // To show the current flight path

  constructor() {
    this.createAircraftMarkerIcon();
  }

  /**
   * Initialize a map with a center latitude and longitude
   */
  initializeMap() {
    const map = L.map('map', {
      center: [environment.initialMapLatitude, environment.initialMapLongitude],
      zoom: environment.initialMapZoom
    });

    const tiles = L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
      maxZoom: 18,
      minZoom: 3,
      attribution: '&copy; <a href="http://www.openstreetmap.org/copyright">OpenStreetMap</a>'
    });

    tiles.addTo(map);

    map.on('load', () => {
      this.loadCurrentFlightPositions(map);
    });

    return map;
  }

  loadCurrentFlightPositions(map: L.Map) {
    const polylineOptions = {
      color: '#f36',
      weight: 6,
      // opacity: 0.9
    };
    if (localStorage.getItem('currentFlightPositions') !== null) {
      this.currentFlightPositions = JSON.parse(localStorage.getItem('currentFlightPositions'));
      this.polyline = L.polyline(this.currentFlightPositions.map((o) => [o.latitude, o.longitude])).addTo(map);
    } else {
      this.polyline = L.polyline([]).addTo(map);
    }
  }

  saveCurrentFlightPositions() {
    localStorage.setItem('currentFlightPositions', JSON.stringify(this.currentFlightPositions));
  }

  clearCurrentFlightPositions(map: L.Map) {
    this.currentFlightPositions = [];
    this.saveCurrentFlightPositions();
    // Clear aircraft marker and polyline if defined
    if (this.aircraftMarker !== null) {
      map.removeLayer(this.aircraftMarker);
      this.aircraftMarker = null;
    }
    if (this.polyline !== null) {
      map.removeLayer(this.polyline);
      this.polyline = null;
    }
  }

  addPositionToCurrentFlight(map: L.Map, position: GpsPosition) {
    if (typeof this.polyline !== 'undefined' && this.polyline !== null) {
      // Add a new position to polyline
      this.polyline.addLatLng([position.latitude, position.longitude]);
    } else {
      // Initialize a new polyline
      this.loadCurrentFlightPositions(map);
    }
    if (typeof this.aircraftMarker !== 'undefined' && this.aircraftMarker !== null) {
      // Update marker position
      this.aircraftMarker.setLatLng([position.latitude, position.longitude]);
    } else {
      // Initialize marker
      this.locateAircraftInMap(map, position);
    }
    this.currentFlightPositions.push(position);
    this.saveCurrentFlightPositions();
  }

  createAircraftMarkerIcon() {
    // @ts-ignore
    this.aircraftIcon = L.ExtraMarkers.icon({
      icon: 'fa-plane',
      markerColor: 'blue-dark',
      shape: 'circle',
      svg: true,
      prefix: 'fas'
    });
  }

  locateAircraftInMap(map: L.Map, position: GpsPosition) {
    // Create marker
    this.aircraftMarker = L.marker([position.latitude, position.longitude], {icon: this.aircraftIcon}).addTo(map);

  }

  panTo(map: L.Map, position: GpsPosition) {
    map.panTo(new L.LatLng(position.latitude, position.longitude));
  }
}
