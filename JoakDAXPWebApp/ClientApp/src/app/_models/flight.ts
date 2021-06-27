import {FlightEventType} from './flightEventType';

export interface Flight {
  id: number;
  eventDateTime: Date;
  flightEventType: FlightEventType;
  location: string;
  latitude: number;
  longitude: number;
  distanceFromIdeal: number;
  glideslopeScore: number;
  verticalSpeed: number;
  maxForce: number;
  pitch: number;
}
