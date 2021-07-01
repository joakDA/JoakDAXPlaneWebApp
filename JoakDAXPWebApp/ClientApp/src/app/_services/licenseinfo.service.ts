import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {environment} from '../../environments/environment';
import {Observable} from 'rxjs';
import {License} from '../_models/license';

@Injectable({
  providedIn: 'root'
})
export class LicenseinfoService {
  constructor(private http: HttpClient) { }

  getAll(): Observable<License[]> {
    return this.http.get<License[]>(`${environment.apiUrl}/api/licenseinfo`);
  }
}
