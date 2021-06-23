import {Component, OnInit, AfterViewInit} from '@angular/core';
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

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit, AfterViewInit {
  private settings: any;
  private airspeed: any;
  private attitude: any;
  private altimeter: any;
  private turnCoordinator: any;
  private heading: any;
  private variometer: any;
  private map;

  constructor(private signalRService: SignalRService, private mapService: MapService) {
    this.settings = {
      off_flag: true,
      size: 250,
      showBox: false,
      showScrews: true,
      img_directory: '/assets/img/'
    };
  }

  ngOnInit() {
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

    this.signalRService.onSignalRMessage.subscribe((data: XPlaneData) => {
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
    this.signalRService.onSignalRTimeout.subscribe(() => {
      // Clear widgets data
      this.clearWidgetsData();
      this.mapService.clearCurrentFlightPositions(this.map);
    });
  }

  ngAfterViewInit(): void {
    this.map = this.mapService.initializeMap();
    // this.mapService.loadCurrentFlightPositions(this.map);
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
    this.altimeter.setPressure(dataAtmosphere.ambientPressure);
  }

  updateDataAttitude(dataAttitude: DtoAttitude) {
    this.attitude.setPitch(dataAttitude.pitch);
    this.attitude.setRoll(dataAttitude.roll * -1);
    this.heading.setHeading(dataAttitude.headingTrue);
    this.turnCoordinator.setTurn(dataAttitude.headingTrue * -1);
  }

  updateDataSpeed(dataSpeed: DtoSpeed) {
    this.airspeed.setAirSpeed(dataSpeed.vIndKts);
  }

  updateDataMachVvi(dataMachVvi: DtoMachVvi) {
    this.variometer.setVario(dataMachVvi.verticalSpeed);
  }

  updateDataPosition(dataPosition: DtoPosition) {
    this.altimeter.setAltitude(dataPosition.altitudeSeaLevel);
    // Update map
    const position: GpsPosition = {latitude: dataPosition.latitude, longitude: dataPosition.longitude};
    this.mapService.addPositionToCurrentFlight(this.map, position);
    this.mapService.panTo(this.map, position);
  }

  updateTimesData(dataTimes: DtoTimes) {
    document.getElementById('zuluTime').innerText = moment(dataTimes.zuluTime).format('DD/MM/YYYY HH:mm:ss');
    document.getElementById('localTime').innerText = moment(dataTimes.localTime).format('DD/MM/YYYY HH:mm:ss');
    // tslint:disable-next-line:max-line-length
    document.getElementById('missionTime').innerText = dataTimes.missionTime.hours + ':' + dataTimes.missionTime.minutes + ':' + dataTimes.missionTime.seconds;
  }

  updatePositionData(data: DtoLocationVelocityDistanceTraveled, altitudeData: DtoPosition) {
    document.getElementById('distanceTraveled').innerText = data.distanceNm + ' nm';
    document.getElementById('altitudeSeaLevel').innerText = altitudeData.altitudeSeaLevel + ' ft';
    document.getElementById('altitudeGroundLevel').innerText = altitudeData.altitudeGroundLevel + ' ft';
    document.getElementById('onRunway').innerText = (altitudeData.runway ? 'Yes' : 'No');
  }

  updateSpeedData(dataMachVvi: DtoMachVvi, dataSpeed: DtoSpeed) {
    document.getElementById('machSpeed').innerText = dataMachVvi.mach.toString();
    document.getElementById('indicatedSpeed').innerText = dataSpeed.vIndKts + ' kts';
    document.getElementById('verticalSpeed').innerText = dataMachVvi.verticalSpeed + ' fpm';
  }

  updateWeatherData(dataWeather: DtoWeather) {
    document.getElementById('slPressure').innerText = dataWeather.seaLevelPressure + ' inHg';
    document.getElementById('slTemperature').innerText = dataWeather.seaLevelTemperature + ' ºC';
    document.getElementById('windSpeed').innerText = dataWeather.windSpeed + ' knots ';
    document.getElementById('windDirection').innerText = dataWeather.windDirection + ' º';
    document.getElementById('turbulence').innerText = dataWeather.turbulence.toString();
    document.getElementById('precipitation').innerText = dataWeather.precipitation.toString();
  }

  updateAtmosphere(dataAtmosphere: DtoAtmosphere) {
    document.getElementById('ambientPressure').innerText = dataAtmosphere.ambientPressure + ' inHg';
    document.getElementById('ambientTemperature').innerText = dataAtmosphere.ambientTemperature + ' ºC';
  }

  updateSystemPressures(dataSystemPressures: DtoSystemPressures) {
    document.getElementById('barometricPressure').innerText = dataSystemPressures.barometricPressure + ' inHg';
  }

  updateTrimFlapsSlatsSpeedBrakes(dataTrimFlapsSlatsSpeedBrakes: DtoTrimFlapsSlatsSpeedBrakes) {
    document.getElementById('flapPosition').innerText = dataTrimFlapsSlatsSpeedBrakes.flapPosition.toString();
    document.getElementById('speedBrakePosition').innerText = dataTrimFlapsSlatsSpeedBrakes.speedBrakePosition.toString();
  }

  updateLandingGearBrakes(dataLandingGearBrakes: DtoLandingGearBrakes) {
    document.getElementById('gearExtended').innerText = (dataLandingGearBrakes.gearExtended ? 'Yes' : 'No');
  }
}
