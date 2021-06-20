import { Injectable} from '@angular/core';
import {Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree} from '@angular/router';

import { AccountService } from '../_services';
import {Observable} from 'rxjs';

@Injectable({ providedIn: 'root' })
export class AuthGuard implements CanActivate {
  constructor(
    private router: Router,
    private accountService: AccountService
  ) {}

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot):
    Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    const user = this.accountService.userValue;
    if (user) {
      // User is authorized, so return true
      return true;
    }

    // not logged in the application, so redirect to login page with a return url.
    this.router.navigate(['account/login'], { queryParams: {returnUrl: state.url }});
    return false;
  }
}
