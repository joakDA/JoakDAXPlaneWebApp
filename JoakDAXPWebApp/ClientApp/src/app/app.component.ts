import {Component, OnInit} from '@angular/core';
import {User} from './_models';
import {AccountService} from './_services';
import {SignalRService} from './_services/signal-r.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'JoakDAXPWebApp';
  user: User;

  constructor(private accountService: AccountService) {
    this.accountService.user.subscribe(x => this.user = x);
  }

  ngOnInit() {
  }

  logout() {
    this.accountService.logout();
  }
}
