import {AfterViewInit, Component, OnDestroy, OnInit} from '@angular/core';
import * as L from 'leaflet';
import {SignalRService} from '../_services/signal-r.service';
import {XPlaneData} from '../_models/signalr/xPlaneData';
import {MapService} from '../_services/map.service';
import {GpsPosition} from '../_models/gpsPosition';
import {DtoPosition} from '../_models/xplane/dtoPosition';
import {Subscription} from 'rxjs';
import * as moment from 'moment';

@Component({
  selector: 'app-map',
  templateUrl: './map.component.html',
  styleUrls: ['./map.component.css']
})
export class MapComponent implements OnInit, AfterViewInit, OnDestroy {
  private historicMap: L.Map;
  private signalRSubscription: Subscription;

  startDate = moment().startOf('day');
  endDate = moment().endOf('day');
  startOptions: any = {format: 'DD/MM/YYYY HH:mm:ss', locale: 'es', sideBySide: true, widgetPositioning: {horizontal: 'right', vertical: 'auto'}};
  endOptions: any = {format: 'DD/MM/YYYY HH:mm:ss', locale: 'es', sideBySide: true, widgetPositioning: {horizontal: 'right', vertical: 'auto'}};

  constructor(private signalRService: SignalRService, private mapService: MapService) { }

  ngOnInit(): void {
    this.signalRService.startConnection();
    this.signalRService.addTransferXPlaneDataListener();
    this.signalRSubscription = this.signalRService.onSignalRMessage.subscribe((data: XPlaneData) => {
      this.updatePositionOnMap(data.dataPosition);
    });

    // Linked datepickers
    this.endOptions.minDate = this.startDate;
    this.startOptions.maxDate = this.endDate;
  }

  ngAfterViewInit(): void {
    this.historicMap = this.mapService.initializeMap();
  }

  private updatePositionOnMap(dataPosition: DtoPosition) {
    try {
      const position: GpsPosition = {latitude: dataPosition.latitude, longitude: dataPosition.longitude};
      this.mapService.addPositionToCurrentFlight(this.historicMap, position);
      this.mapService.panTo(this.historicMap, position);
    } catch (e) {
      console.log('Update Position Historic Map: Ko. Exception details: ' + e.message);
    }
  }

  ngOnDestroy(): void {
    if (this.signalRSubscription) {
      this.signalRSubscription.unsubscribe();
    }
    this.signalRService.stopConnection();
  }

  onUpdateDatepicker() {
    // Linked datepickers
    this.startOptions.maxDate = this.endDate;
    this.endOptions.minDate = this.startDate;
  }
}
