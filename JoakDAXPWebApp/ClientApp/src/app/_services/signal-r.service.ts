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
  // tslint:disable-next-line:max-line-length
  private receptionTimeout; // Used to reset monitoring values if no data is received in a period of time because XPlane is configured to send continuous data.

  @Output() onSignalRMessage: EventEmitter<any> = new EventEmitter();
  @Output() onSignalRTimeout: EventEmitter<any> = new EventEmitter();

  public startConnection = () => {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(`${environment.signalRHub}`)
      .withAutomaticReconnect()
      .configureLogging(signalR.LogLevel.Information)
      .build();

    this.hubConnection
      .start()
      .then(() => console.log('Connection to hub started...'))
      .catch(err => console.log('Error while starting connection to hub: ' + err));
  }

  public addTransferXPlaneDataListener = () => {
    this.hubConnection.on('xplanedata', (data: XPlaneData) => {
      if (this.receptionTimeout !== null) {
        // If new data is received, clear the timeout is defined.
        clearTimeout(this.receptionTimeout);
      }
      this.xPlateDataReceived(data as XPlaneData);
      this.receptionTimeout = setTimeout(() => {
        this.onNoRealTimeDataReceived();
      }, environment.receptionTimeoutSignalR);
    });
  }

  private onNoRealTimeDataReceived() {
    this.onSignalRTimeout.emit();
  }

  private xPlateDataReceived(data: XPlaneData) {
    this.onSignalRMessage.emit(data);
  }

  constructor() { }
}
