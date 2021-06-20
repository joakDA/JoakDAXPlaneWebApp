import {DtoData} from './dtoData';

export interface DtoTrimFlapsSlatsSpeedBrakes extends DtoData {
  elevatorTrim: number;
  aileronTrim: number;
  ruddlerTrim: number;
  flapPosition: number;
  speedBrakePosition: number;
}
