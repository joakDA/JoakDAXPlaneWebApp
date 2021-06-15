import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { faPlane, faHome, faPlaneDeparture, faMapMarker, faCogs, faInfo, faBars, faArrowLeft } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-aside',
  templateUrl: './aside.component.html',
  styleUrls: ['./aside.component.css']
})
export class AsideComponent implements OnInit {
  faBarsIcon = faBars;
  faPlaneIcon = faPlane;
  faHomeIcon = faHome;
  faPlaneDepartureIcon = faPlaneDeparture;
  faMapMarkerIcon = faMapMarker;
  faCogsIcon = faCogs;
  faInfoIcon = faInfo;
  faArrowLeftIcon = faArrowLeft

  isCollapsed = true;

  collapse() {
    this.isCollapsed = true;
  }

  toggle() {
    this.isCollapsed = !this.isCollapsed;
  }

  constructor(private router: Router) { }

  ngOnInit() {
  }

  navigateHome() {      
    this.router.navigate(['']);
  }

}
