import {DtoData} from './dtoData';
import {TimeSpan} from '../timespan';

export interface DtoTimes extends DtoData {
  missionTime: TimeSpan;
  zuluTime: Date;
  localTime: Date;
}
