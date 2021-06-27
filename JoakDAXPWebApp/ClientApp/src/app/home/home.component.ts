import {Component, OnInit, AfterViewInit, OnDestroy} from '@angular/core';
import {SignalRService} from '../_services/signal-r.service';
import {XPlaneData} from '../_models/signalr/xPlaneData';
import {DtoSpeed} from '../_models/xplane/dtoSpeed';
import {DtoAtmosphere} from '../_models/xplane/dtoAtmosphere';
import {DtoAttitude} from '../_models/xplane/dtoAttitude';
import {DtoMachVvi} from '../_models/xplane/dtoMachVvi';
import {DtoPosition} from '../_models/xplane/dtoPosition';
import {DtoTimes} from '../_models/xplane/dtoTimes';
import * as moment from 'moment';
import {DtoLocationVelocityDistanceTraveled} from '../_models/xplane/dtoLocationVelocityDistanceTraveled';
import {DtoWeather} from '../_models/xplane/dtoWeather';
import {DtoSystemPressures} from '../_models/xplane/dtoSystemPressures';
import {DtoTrimFlapsSlatsSpeedBrakes} from '../_models/xplane/dtoTrimFlapsSlatsSpeedBrakes';
import {DtoLandingGearBrakes} from '../_models/xplane/dtoLandingGearBrakes';
import {MapService} from '../_services/map.service';
import {GpsPosition} from '../_models/gpsPosition';
import {NavigationStart, Router} from '@angular/router';
import {Subscription} from 'rxjs';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit, AfterViewInit, OnDestroy {
  private signalRSubscription: Subscription;
  private signalRTimeoutSubscription: Subscription;
  private router: Router;
  private settings: any;
  private airspeed: any;
  private attitude: any;
  private altimeter: any;
  private turnCoordinator: any;
  private heading: any;
  private variometer: any;
  private map;

  constructor(private signalRService: SignalRService, private mapService: MapService, private routerElem: Router) {
    this.router = routerElem;
    this.settings = {
      off_flag: true,
      size: 250,
      showBox: false,
      showScrews: true,
      img_directory: '/assets/img/'
    };
  }

  ngOnInit() {
    this.signalRService.startConnection();
    this.signalRService.addTransferXPlaneDataListener();

    // @ts-ignore
    this.airspeed = $.flightIndicator('#airspeed', 'airspeed', this.settings);
    // @ts-ignore
    this.attitude = $.flightIndicator('#attitude', 'attitude', this.settings);
    // @ts-ignore
    this.altimeter = $.flightIndicator('#altimeter', 'altimeter', this.settings);
    // @ts-ignore
    this.turnCoordinator = $.flightIndicator('#turnCoordinator', 'turn_coordinator', this.settings);
    // @ts-ignore
    this.heading = $.flightIndicator('#heading', 'heading', this.settings);
    // @ts-ignore
    this.variometer = $.flightIndicator('#variometer', 'variometer', this.settings);

    this.signalRSubscription = this.signalRService.onSignalRMessage.subscribe((data: XPlaneData) => {
      // console.log(data);
      this.updateDataAtmosphere(data.dataAtmosphere);
      this.updateDataAttitude(data.dataAttitude);
      this.updateDataSpeed(data.dataSpeed);
      this.updateDataMachVvi(data.dataMachVvi);
      this.updateDataPosition(data.dataPosition);
      this.updateTimesData(data.dataTimes);
      this.updatePositionData(data.dataLocationVelocityDistanceTraveled, data.dataPosition);
      this.updateSpeedData(data.dataMachVvi, data.dataSpeed);
      this.updateWeatherData(data.dataWeather);
      this.updateAtmosphere(data.dataAtmosphere);
      this.updateSystemPressures(data.dataSystemPressures);
      this.updateTrimFlapsSlatsSpeedBrakes(data.dataTrimFlapsSlatsSpeedBrakes);
      this.updateLandingGearBrakes(data.dataLandingGearBrakes);
    });

    // Timeout event subscriber
    this.signalRTimeoutSubscription = this.signalRService.onSignalRTimeout.subscribe(() => {
      // Clear widgets data
      this.clearWidgetsData();
      this.mapService.clearCurrentFlightPositions(this.map);
    });

    this.router.events.subscribe(event => {
      if (event instanceof NavigationStart) {
        console.log(event.url);
        this.routerChangeMethod(event.url);
      }
    });
  }

  routerChangeMethod(url) {
    console.log('Changed to url: ' + url);
    // TODO: Depending of the route, execute some actions
  }

  ngAfterViewInit(): void {
    this.map = this.mapService.initializeMap();
    // this.mapService.loadCurrentFlightPositions(this.map);
  }

  ngOnDestroy(): void {
    if (this.signalRSubscription) {
      this.signalRSubscription.unsubscribe();
    }
    if (this.signalRTimeoutSubscription) {
      this.signalRTimeoutSubscription.unsubscribe();
    }
    this.signalRService.stopConnection();
  }

  clearWidgetsData() {
    try {
      const dataValueElems = document.getElementsByClassName('data-value');
      for (let i = 0; i < dataValueElems.length; i++) {
        dataValueElems[i].innerHTML = 'N/A';
      }
    } catch (e) {
      console.log('Clear Widgets Data: Ko. Exception details: ' + e.message);
    }
  }


  updateDataAtmosphere(dataAtmosphere: DtoAtmosphere) {
    try {
      this.altimeter.setPressure(dataAtmosphere.ambientPressure);
    } catch (e) {
      console.log('Update Data Atmosphere: Ko. Exception details: ' + e.message);
    }
  }

  updateDataAttitude(dataAttitude: DtoAttitude) {
    try {
      this.attitude.setPitch(dataAttitude.pitch);
      this.attitude.setRoll(dataAttitude.roll * -1);
      this.heading.setHeading(dataAttitude.headingTrue);
      this.turnCoordinator.setTurn(dataAttitude.headingTrue * -1);
    } catch (e) {
      console.log('Update Data Attitude: Ko. Exception details: ' + e.message);
    }
  }

  updateDataSpeed(dataSpeed: DtoSpeed) {
    try {
      this.airspeed.setAirSpeed(dataSpeed.vIndKts);
    } catch (e) {
      console.log('Update Data Speed: Ko. Exception details: ' + e.message);
    }
  }

  updateDataMachVvi(dataMachVvi: DtoMachVvi) {
    try {
    this.variometer.setVario(dataMachVvi.verticalSpeed);
    } catch (e) {
      console.log('Update Data Mach Vvi: Ko. Exception details: ' + e.message);
    }
  }

  updateDataPosition(dataPosition: DtoPosition) {
    try {
      this.altimeter.setAltitude(dataPosition.altitudeSeaLevel);
      // Update map
      const position: GpsPosition = {latitude: dataPosition.latitude, longitude: dataPosition.longitude};
      this.mapService.addPositionToCurrentFlight(this.map, position);
      this.mapService.panTo(this.map, position);
    } catch (e) {
      console.log('Update Position: Ko. Exception details: ' + e.message);
    }
  }

  updateTimesData(dataTimes: DtoTimes) {
    try {
      document.getElementById('zuluTime').innerText = moment(dataTimes.zuluTime).format('DD/MM/YYYY HH:mm:ss');
      document.getElementById('localTime').innerText = moment(dataTimes.localTime).format('DD/MM/YYYY HH:mm:ss');
      // tslint:disable-next-line:max-line-length
      document.getElementById('missionTime').innerText = dataTimes.missionTime.hours + ':' + dataTimes.missionTime.minutes + ':' + dataTimes.missionTime.seconds;
    } catch (e) {
      console.log('Update Times: Ko. Exception details: ' + e.message);
    }
  }

  updatePositionData(data: DtoLocationVelocityDistanceTraveled, altitudeData: DtoPosition) {
    try {
      document.getElementById('distanceTraveled').innerText = data.distanceNm + ' nm';
      document.getElementById('altitudeSeaLevel').innerText = altitudeData.altitudeSeaLevel + ' ft';
      document.getElementById('altitudeGroundLevel').innerText = altitudeData.altitudeGroundLevel + ' ft';
      document.getElementById('onRunway').innerText = (altitudeData.runway ? 'Yes' : 'No');
    } catch (e) {
      console.log('Update Position Data: Ko. Exception details: ' + e.message);
    }
  }

  updateSpeedData(dataMachVvi: DtoMachVvi, dataSpeed: DtoSpeed) {
    try {
      document.getElementById('machSpeed').innerText = dataMachVvi.mach.toString();
      document.getElementById('indicatedSpeed').innerText = dataSpeed.vIndKts + ' kts';
      document.getElementById('verticalSpeed').innerText = dataMachVvi.verticalSpeed + ' fpm';
    } catch (e) {
      console.log('Update Speed Data: Ko. Exception details: ' + e.message);
    }
  }

  updateWeatherData(dataWeather: DtoWeather) {
    try {
      document.getElementById('slPressure').innerText = dataWeather.seaLevelPressure + ' inHg';
      document.getElementById('slTemperature').innerText = dataWeather.seaLevelTemperature + ' ºC';
      document.getElementById('windSpeed').innerText = dataWeather.windSpeed + ' knots ';
      document.getElementById('windDirection').innerText = dataWeather.windDirection + ' º';
      document.getElementById('turbulence').innerText = dataWeather.turbulence.toString();
      document.getElementById('precipitation').innerText = dataWeather.precipitation.toString();
    } catch (e) {
      console.log('Update Weather Data: Ko. Exception details: ' + e.message);
    }
  }

  updateAtmosphere(dataAtmosphere: DtoAtmosphere) {
    try {
      document.getElementById('ambientPressure').innerText = dataAtmosphere.ambientPressure + ' inHg';
      document.getElementById('ambientTemperature').innerText = dataAtmosphere.ambientTemperature + ' ºC';
    } catch (e) {
      console.log('Update Data Atmosphere: Ko. Exception details: ' + e.message);
    }
  }

  updateSystemPressures(dataSystemPressures: DtoSystemPressures) {
    try {
      document.getElementById('barometricPressure').innerText = dataSystemPressures.barometricPressure + ' inHg';
    } catch (e) {
      console.log('Update System Pressures Data: Ko. Exception details: ' + e.message);
    }
  }

  updateTrimFlapsSlatsSpeedBrakes(dataTrimFlapsSlatsSpeedBrakes: DtoTrimFlapsSlatsSpeedBrakes) {
    try {
      document.getElementById('flapPosition').innerText = dataTrimFlapsSlatsSpeedBrakes.flapPosition.toString();
      document.getElementById('speedBrakePosition').innerText = dataTrimFlapsSlatsSpeedBrakes.speedBrakePosition.toString();
    } catch (e) {
      console.log('Update Trim Flaps Slats Speed Brakes: Ko. Exception details: ' + e.message);
    }
  }

  updateLandingGearBrakes(dataLandingGearBrakes: DtoLandingGearBrakes) {
    try {
      document.getElementById('gearExtended').innerText = (dataLandingGearBrakes.gearExtended ? 'Yes' : 'No');
    } catch (e) {
      console.log('Update Landing Gear Brakes: Ko. Exception details: ' + e.message);
    }
  }
}
