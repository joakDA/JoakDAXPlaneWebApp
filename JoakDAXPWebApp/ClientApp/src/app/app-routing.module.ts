import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { HomeComponent} from './home/home.component';
import { AuthGuard } from './_helpers/auth.guard';
import {FlightComponent} from './flight/flight.component';
import {MapComponent} from './map/map.component';
import {LicenseInfoComponent} from './license-info/license-info.component';

const accountModule = () => import('./account/account.module').then(x => x.AccountModule);
const usersModule = () => import('./users/users/users.module').then(x => x.UsersModule);

const routes: Routes = [
    { path: '', component: HomeComponent, canActivate: [AuthGuard] },
    { path: 'flights', component: FlightComponent, canActivate: [AuthGuard] },
    { path: 'map', component: MapComponent, canActivate: [AuthGuard] },
    { path: 'users', loadChildren: usersModule },
    { path: 'account', loadChildren: accountModule },
    { path: 'licenses', component: LicenseInfoComponent },

    // otherwise redirect to home
    { path: '**', redirectTo: '' }
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule { }
