import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {DataTablesResponse} from '../_models/data-tables-response';
import {environment} from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class FlightService {

  constructor(private http: HttpClient) { }

  getAll(requestData: any) {
    return this.http.post<DataTablesResponse>(`${environment.apiUrl}/api/flights`, requestData);
  }
}
