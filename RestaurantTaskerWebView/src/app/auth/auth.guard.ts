import {Injectable} from '@angular/core';
import {CanActivate, ActivatedRouteSnapshot, Router, RouterStateSnapshot} from '@angular/router';
import {AuthService} from '../shared/auth-sevice/auth.service';
import { from } from 'rxjs';

@Injectable({
    providedIn: 'root'
})

export class AuthGuard implements CanActivate {
   
    constructor(private router: Router, private authService: AuthService){}

    canActivate(
        next: ActivatedRouteSnapshot,
        state: RouterStateSnapshot
    ) : boolean {
        if (this.authService.isLoggedIn())
        return true;

        else{
            this.router.navigate(['/user/login']);
            return false;
        }
    }

    /*canActivate(
        next: ActivatedRouteSnapshot,
        state: RouterStateSnapshot
    ) : boolean {
        if (localStorage.getItem('token') != null)
        return true;

        else{
            this.router.navigate(['/user/login']);
            return false;
        }
    }*/
}