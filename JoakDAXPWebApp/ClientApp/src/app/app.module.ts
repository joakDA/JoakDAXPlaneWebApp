import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { AppComponent } from './app.component';
import { AsideComponent } from './layout/aside/aside.component';
import { FooterComponent } from './layout/footer/footer.component';
import { HomeComponent } from './home/home.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { AlertComponent } from './alert/alert.component';
import { ErrorInterceptor } from './_helpers/error.interceptor';
import { JwtInterceptor } from './_helpers/jwt.interceptor';
import {AppRoutingModule} from './app-routing.module';
import { LoaderService } from './_services/loader.service';
import { LoaderInterceptorService } from './_helpers/loader-interceptor.service';
import { SpinnerComponent } from './layout/spinner/spinner.component';
import { FlightComponent } from './flight/flight.component';
import {DataTablesModule} from 'angular-datatables';
import { MapComponent } from './map/map.component';
import {NgTempusdominusBootstrapModule} from 'ngx-tempusdominus-bootstrap';
import { LicenseInfoComponent } from './license-info/license-info.component';
import {AccordionModule} from 'ngx-bootstrap/accordion';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';

@NgModule({
  declarations: [
    AppComponent,
    AsideComponent,
    FooterComponent,
    AlertComponent,
    HomeComponent,
    SpinnerComponent,
    FlightComponent,
    MapComponent,
    LicenseInfoComponent
  ],
  imports: [
    BrowserModule.withServerTransition({appId: 'ng-cli-universal'}),
    HttpClientModule,
    ReactiveFormsModule,
    FontAwesomeModule,
    AppRoutingModule,
    DataTablesModule,
    NgTempusdominusBootstrapModule,
    BrowserAnimationsModule,
    AccordionModule.forRoot(),
    FormsModule
  ],
  providers: [
    LoaderService,
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true},
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true},
    { provide: HTTP_INTERCEPTORS, useClass: LoaderInterceptorService, multi: true}
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
