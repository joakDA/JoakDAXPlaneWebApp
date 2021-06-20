import {DtoData} from './dtoData';

export interface DtoAttitude extends DtoData {
  pitch: number;
  roll: number;
  headingTrue: number;
}
