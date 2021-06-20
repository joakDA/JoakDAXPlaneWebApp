import {DtoData} from './dtoData';

export interface DtoWeather extends DtoData {
  seaLevelPressure: number;
  seaLevelTemperature: number;
  windSpeed: number;
  windDirection: number;
  turbulence: number;
  precipitation: number;
}
