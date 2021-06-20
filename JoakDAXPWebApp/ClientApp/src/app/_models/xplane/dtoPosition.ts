import {DtoData} from './dtoData';

export interface DtoPosition extends DtoData {
  latitude: number;
  longitude: number;
  altitudeSeaLevel: number;
  altitudeGroundLevel: number;
  runway: boolean;
}
