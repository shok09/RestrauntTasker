import { Injectable } from '@angular/core';
import { Observable, empty, of, from } from 'rxjs';
import { HttpClient, HttpHeaders} from "@angular/common/http";
import { mapTo, catchError, tap} from 'rxjs/operators';
import {Tokens} from './models/tokens';

@Injectable({
  providedIn: 'root'
})

export class AuthService{

    private readonly JWT_TOKEN = 'JWT_TOKEN';
    private readonly REFRESH_TOKEN = 'REFRESH_TOKEN';
    private readonly BASE_URL = "http://localhost:5001";
    private loggedUser: string;

    constructor(private http: HttpClient){}

    login(user: { username: string, password: string }): Observable<boolean> {
        return this.http.post<any>(`${this.BASE_URL}/auth/login`, user)
          .pipe(
            tap(tokens => this.doLoginUser(user.username, tokens)),
            mapTo(true),
            catchError(error => {
              alert(error.error);
              return of(false);
            }));
            
      }

      logout() {
        return this.http.post<any>(`${this.BASE_URL}/logout`, {
          'refreshToken': this.getRefreshToken()
        }).pipe(
          tap(() => {
            this.doLogoutUser();
            console.log(this.getJwtToken());
          }),
          mapTo(true),
          catchError(error => {
            alert(error.error);
            return of(false);
          }));
      }

      refreshToken() {
        return this.http.post<any>(`${this.BASE_URL}/auth/refreshToken`, {
          'refreshToken': this.getRefreshToken()
        }).pipe(tap((tokens: Tokens) => {
          this.storeJwtToken(tokens.accessToken.token);
          this.storeJwtToken(tokens.refreshToken);
          this.storeJwtToken(tokens.accessToken.expiresIn.toString())
        }));
      }

      isLoggedIn() {
        return !!this.getJwtToken();
      }

      getJwtToken() {
        return localStorage.getItem(this.JWT_TOKEN);
      }

      private doLoginUser(username: string, tokens: Tokens) {
        this.loggedUser = username;
        this.storeTokens(tokens);
      }

      private doLogoutUser() {
        this.loggedUser = null;
        this.removeTokens();
      }

      private getRefreshToken() {
        return localStorage.getItem(this.REFRESH_TOKEN);
      }
    
      private storeJwtToken(jwt: string) {
        localStorage.setItem(this.JWT_TOKEN, jwt);
      }
    
      private storeTokens(tokens: Tokens) {
        localStorage.setItem(this.JWT_TOKEN, tokens.accessToken.token);
        localStorage.setItem(this.REFRESH_TOKEN, tokens.refreshToken);
      }
    
      private removeTokens() {
        localStorage.removeItem(this.JWT_TOKEN);
        localStorage.removeItem(this.REFRESH_TOKEN);
      }
    }
    