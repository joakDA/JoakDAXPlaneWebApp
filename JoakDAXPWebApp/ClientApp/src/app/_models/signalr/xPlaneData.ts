import {DtoAtmosphere} from '../xplane/dtoAtmosphere';
import {DtoAttitude} from '../xplane/dtoAttitude';
import {DtoFrameRate} from '../xplane/dtoFrameRate';
import {DtoLandingGearBrakes} from '../xplane/dtoLandingGearBrakes';
import {DtoLocationVelocityDistanceTraveled} from '../xplane/dtoLocationVelocityDistanceTraveled';
import {DtoMachVvi} from '../xplane/dtoMachVvi';
import {DtoPosition} from '../xplane/dtoPosition';
import {DtoSpeed} from '../xplane/dtoSpeed';
import {DtoSystemPressures} from '../xplane/dtoSystemPressures';
import {DtoTimes} from '../xplane/dtoTimes';
import {DtoTrimFlapsSlatsSpeedBrakes} from '../xplane/dtoTrimFlapsSlatsSpeedBrakes';
import {DtoWeather} from '../xplane/dtoWeather';

export interface XPlaneData {
  dataAtmosphere: DtoAtmosphere;
  dataAttitude: DtoAttitude;
  dataFrameRate: DtoFrameRate;
  dataLandingGearBrakes: DtoLandingGearBrakes;
  dataLocationVelocityDistanceTraveled: DtoLocationVelocityDistanceTraveled;
  dataMachVvi: DtoMachVvi;
  dataPosition: DtoPosition;
  dataSpeed: DtoSpeed;
  dataSystemPressures: DtoSystemPressures;
  dataTimes: DtoTimes;
  dataTrimFlapsSlatsSpeedBrakes: DtoTrimFlapsSlatsSpeedBrakes;
  dataWeather: DtoWeather;
}
