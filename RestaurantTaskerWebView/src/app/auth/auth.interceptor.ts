import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpErrorResponse } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, from, throwError, BehaviorSubject } from "rxjs";
import { tap, catchError, take, switchMap, filter } from "rxjs/operators";
import { Router } from "@angular/router";
import { AuthService} from '../shared/auth-sevice/auth.service';
@Injectable()
export class AuthInterceptor implements HttpInterceptor {

    constructor(private router: Router, public authService: AuthService) {}

    private isRefreshing = false;
    private refreshTokenSubject: BehaviorSubject<any> = new BehaviorSubject<any>(null);

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

        if (this.authService.getJwtToken()) {
          request = this.addToken(request, this.authService.getJwtToken());
        }
    
        return next.handle(request).pipe(catchError(error => {
          if (error instanceof HttpErrorResponse && error.status === 401) {
            return this.handle401Error(request, next);
          } else {
            return throwError(error);
          }
        }));
      }
    
      private addToken(request: HttpRequest<any>, token: string) {
        return request.clone({
          setHeaders: {
            Authorization: `Bearer ${token}`
          }
        });
      }

      private handle401Error(request: HttpRequest<any>, next: HttpHandler) {
        if (!this.isRefreshing) {
          this.isRefreshing = true;
          this.refreshTokenSubject.next(null);
    
          return this.authService.refreshToken().pipe(
            switchMap((token: any) => {
              this.isRefreshing = false;
              this.refreshTokenSubject.next(token.jwt);
              return next.handle(this.addToken(request, token.jwt));
            }));
    
        } else {
          return this.refreshTokenSubject.pipe(
            filter(token => token != null),
            take(1),
            switchMap(jwt => {
              return next.handle(this.addToken(request, jwt));
            }));
        }
      }
    

    /*intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        if (localStorage.getItem('token') != null) {
            const clonedReq = req.clone({
                headers: req.headers.set('Authorization', 'Bearer ' + localStorage.getItem('token'))
            });
            return next.handle(clonedReq).pipe(
                tap(
                    succ => { },
                    err => {
                        if (err.status == 401){
                            localStorage.removeItem('token');
                            this.router.navigateByUrl('/user/login');
                        }
                    }
                )
            )
        }
        else
            return next.handle(req.clone());
    }*/
}