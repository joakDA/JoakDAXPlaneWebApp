import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {ReactiveFormsModule} from '@angular/forms';
import {UsersRoutingModule} from '../users-routing/users-routing.module';
import {LayoutComponent} from '../layout/layout.component';
import {ListComponent} from '../list/list.component';
import {ProfileComponent} from '../profile/profile.component';
import {HTTP_INTERCEPTORS} from '@angular/common/http';
import {ErrorInterceptor} from '../../_helpers/error.interceptor';
import {JwtInterceptor} from '../../_helpers/jwt.interceptor';
import {DataTablesModule} from 'angular-datatables';
import {AvatarModule} from 'ngx-avatar';

@NgModule({
  imports: [
    CommonModule,
    ReactiveFormsModule,
    DataTablesModule,
    UsersRoutingModule,
    AvatarModule
  ],
  declarations: [
    LayoutComponent,
    ListComponent,
    ProfileComponent
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true},
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true}
  ],
})
export class UsersModule { }
