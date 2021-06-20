import { NgModule } from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {LayoutComponent} from '../layout/layout.component';
import {ListComponent} from '../list/list.component';
import {AuthGuard} from '../../_helpers/auth.guard';
import {ProfileComponent} from '../profile/profile.component';

const routes: Routes = [
  {
    path: '', component: LayoutComponent,
    children: [
      { path: '', component: ListComponent, canActivate: [ AuthGuard ] },
      { path: 'profile/:id', component: ProfileComponent, canActivate: [AuthGuard] }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UsersRoutingModule { }
