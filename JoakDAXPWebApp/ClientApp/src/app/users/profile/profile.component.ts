import { Component, OnInit } from '@angular/core';
import {AccountService} from '../../_services';
import {User} from '../../_models';
import {first} from 'rxjs/operators';
import * as moment from 'moment';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {
  id: string;
  user: User = null;
  constructor(
    public accountService: AccountService,
  ) { }

  ngOnInit(): void {
    this.id = this.accountService.userValue.id;

    // Retrieve user details from api
    this.accountService.getById(this.id)
      .pipe(first())
      .subscribe(x => this.user = x);
  }

  formatDateToString(dateValue): string {
    return (moment(dateValue)).format('DD/MM/YYYY HH:mm:ss');
  }
}
