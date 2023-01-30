import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from "@angular/router";
import { Observable } from "rxjs";

@Injectable()
export class CanActivatePatron implements CanActivate {
    constructor(
        private router: Router,
    ) { }
    canActivate(
        route: ActivatedRouteSnapshot,
        state: RouterStateSnapshot
    ): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
        if (localStorage.getItem('USER_TYPE') === 'patron') {
            return true;
        } else {
            this.router.navigate(['/login']);
            return false;
        }
    }
}