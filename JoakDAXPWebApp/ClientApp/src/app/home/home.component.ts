import {Component, OnInit} from '@angular/core';
import {SignalRService} from '../_services/signal-r.service';
import {XPlaneData} from '../_models/signalr/xPlaneData';
import {DtoSpeed} from '../_models/xplane/dtoSpeed';
import {DtoAtmosphere} from '../_models/xplane/dtoAtmosphere';
import {DtoAttitude} from '../_models/xplane/dtoAttitude';
import {DtoMachVvi} from '../_models/xplane/dtoMachVvi';
import {DtoPosition} from '../_models/xplane/dtoPosition';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit {
  private settings: any;
  private airspeed: any;
  private attitude: any;
  private altimeter: any;
  private turnCoordinator: any;
  private heading: any;
  private variometer: any;
  constructor(private signalRService: SignalRService) {
    this.settings = {
      off_flag: true,
      size: 300,
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
      this.updateDataAtmosphere(data.dataAtmosphere);
      this.updateDataAttitude(data.dataAttitude);
      this.updateDataSpeed(data.dataSpeed);
      this.updateDataMachVvi(data.dataMachVvi);
      this.updateDataPosition(data.dataPosition);
    });
  }

  updateDataAtmosphere(dataAtmosphere: DtoAtmosphere) {
    this.altimeter.setPressure(dataAtmosphere.ambientPressure);
  }

  updateDataAttitude(dataAttitude: DtoAttitude) {
    this.attitude.setPitch(dataAttitude.pitch);
    this.attitude.setRoll(dataAttitude.roll);
    this.heading.setHeading(dataAttitude.headingTrue * -1);
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
  }
}
