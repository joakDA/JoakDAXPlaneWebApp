import { Component, OnInit } from '@angular/core';
import {License} from '../_models/license';
import {LicenseinfoService} from '../_services/licenseinfo.service';
import {first} from 'rxjs/operators';

@Component({
  selector: 'app-license-info',
  templateUrl: './license-info.component.html',
  styleUrls: ['./license-info.component.css']
})
export class LicenseInfoComponent implements OnInit {
  licenses: License[] = null;
  customClass = 'bg-dark';
  isFirstOpen = true;
  constructor(private licenseService: LicenseinfoService) { }

  ngOnInit(): void {
    this.licenseService.getAll()
      .pipe(first())
      .subscribe(resp => {
        this.licenses = resp;
      });
  }
}
