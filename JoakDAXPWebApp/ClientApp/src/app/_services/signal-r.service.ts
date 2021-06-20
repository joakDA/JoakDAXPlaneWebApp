import {EventEmitter, Injectable, Output} from '@angular/core';
import * as signalR from '@aspnet/signalr';
import {XPlaneData} from '../_models/signalr/xPlaneData';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class SignalRService {
  public data: XPlaneData;
  private hubConnection: signalR.HubConnection;

  @Output() onSignalRMessage: EventEmitter<any> = new EventEmitter();

  public startConnection = () => {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(`${environment.signalRHub}`)
      .build();

    this.hubConnection
      .start()
      .then(() => console.log('Connection to hub started...'))
      .catch(err => console.log('Error while starting connection to hub: ' + err));
  }

  public addTransferXPlaneDataListener = () => {
    this.hubConnection.on('xplanedata', (data: XPlaneData) => {
      this.xPlateDataReceived(data as XPlaneData);
    });
  }

  private xPlateDataReceived(data: XPlaneData) {
    this.onSignalRMessage.emit(data);
  }

  constructor() { }
}
