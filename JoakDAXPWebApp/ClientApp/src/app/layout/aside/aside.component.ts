import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import {
  faPlane,
  faHome,
  faPlaneDeparture,
  faMapMarker,
  faCogs,
  faInfo,
  faArrowLeft,
  faUsers,
  faUserPlus,
  faSignOutAlt,
  faUserAlt
} from '@fortawesome/free-solid-svg-icons';
import {AccountService} from '../../_services';
import {User} from '../../_models';

@Component({
  selector: 'app-aside',
  templateUrl: './aside.component.html',
  styleUrls: ['./aside.component.css']
})
export class AsideComponent implements OnInit {
  user: User;
  faPlaneIcon = faPlane;
  faHomeIcon = faHome;
  faPlaneDepartureIcon = faPlaneDeparture;
  faMapMarkerIcon = faMapMarker;
  faCogsIcon = faCogs;
  faInfoIcon = faInfo;
  faArrowLeftIcon = faArrowLeft;
  faUsersIcon = faUsers;
  faUserIcon = faUserAlt;
  faUserPlusIcon = faUserPlus;
  faUserLogoutIcon = faSignOutAlt;
  isCollapsed = true;

  collapse() {
    this.isCollapsed = true;
  }

  toggle() {
    this.isCollapsed = !this.isCollapsed;
  }

  constructor(private accountService: AccountService, private router: Router) {
    this.user = this.accountService.userValue;
  }

  ngOnInit() {
  }

  navigateHome() {
    this.router.navigate(['']);
  }

  logout() {
    this.accountService.logout();
  }
}
