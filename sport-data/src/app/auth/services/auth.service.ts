import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { GlobalConstants } from '../../core/constants/global-constants';
import { ApiRouteConstants } from '../../core/constants/api-route-constants';
import { IRegistration } from '../interfaces/registration';
import { ILogin } from '../interfaces/login';
import { IUser } from '../interfaces/user';
import { JwtHelperService } from '@auth0/angular-jwt';
import { IToken } from '../interfaces/token';
import { IRefreshToken } from '../interfaces/refresh-token';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private user!: IUser | null;
  private jwtHelper = new JwtHelperService();

  constructor(private httpClient: HttpClient) {
    this.user = this.setUser();
  }

  register(model: IRegistration): Observable<any> {
    return this.httpClient.post(ApiRouteConstants.USERS, model);
  }

  login(model: ILogin): Observable<any> {
    return this.httpClient
      .post(ApiRouteConstants.TOKENS_CREATE, model)
      .pipe(
        tap((res: any) => this.setTokens(res))
      );
  }

  logout() {
    localStorage.removeItem(GlobalConstants.ACCESS_TOKEN);
    localStorage.removeItem(GlobalConstants.REFRESH_TOKEN);
    localStorage.removeItem(GlobalConstants.USERNAME);
    this.user = null;
  }

  createRefreshToken(): Observable<any> {
    const model: IRefreshToken = {
      refreshToken: this.getRefreshToken(),
      username: this.getUsername()
    };

    return this.httpClient.post(ApiRouteConstants.TOKENS_CREATE_REFRESH_TOKEN, model);
  }

  isLoggedIn(): boolean {
    return this.user !== null && this.tokensExist();
  }

  getAccessToken(): string | null {
    return localStorage.getItem(GlobalConstants.ACCESS_TOKEN);
  }

  getRefreshToken(): string | null {
    return localStorage.getItem(GlobalConstants.REFRESH_TOKEN);
  }

  getUsername(): string | null {
    return localStorage.getItem(GlobalConstants.USERNAME);
  }

  isTokenExpired(): boolean {
    const accessToken = localStorage.getItem(GlobalConstants.ACCESS_TOKEN);
    if (!accessToken) {
      return true;
    }

    const isExpired = this.jwtHelper.isTokenExpired(accessToken);
    return isExpired;
  }

  setTokens(token: IToken) {
    localStorage.setItem(GlobalConstants.ACCESS_TOKEN, token.accessToken);
    localStorage.setItem(GlobalConstants.REFRESH_TOKEN, token.refreshToken);
    localStorage.setItem(GlobalConstants.USERNAME, token.username);

    this.user = this.setUser();
  }

  private setUser(): IUser | null {
    const accessToken = localStorage.getItem(GlobalConstants.ACCESS_TOKEN);
    if (accessToken) {
      const payload = this.jwtHelper.decodeToken(accessToken);
      const currentUser: IUser = {
        id: "asdas",
        username: payload[GlobalConstants.JWT_USERNAME],
        role: payload[GlobalConstants.JWT_ROLE]
      };

      return currentUser;
    }

    return null;
  }

  private tokensExist(): boolean {
    const accessToken = localStorage.getItem(GlobalConstants.ACCESS_TOKEN);
    const refreshToken = localStorage.getItem(GlobalConstants.REFRESH_TOKEN);
    const username = localStorage.getItem(GlobalConstants.USERNAME);

    if (accessToken && refreshToken && username) {
      return true;
    }

    return false;
  }


  // updateRefreshToken(): Observable<any> {
  //   const model: IToken = {
  //     accessToken: localStorage.getItem(GlobalConstants.ACCESS_TOKEN),
  //     refreshToken: localStorage.getItem(GlobalConstants.REFRESH_TOKEN),
  //     expiration: null
  //   } 

  //   // const url  = `${environment.apiUrl}${ApiRouteConstants.TOKENS_REFRESH}`;
  //   return this.httpClient.post(ApiRouteConstants.TOKENS_CREATE_REFRESH_TOKEN, model);
  // }

  // createRefreshToken(): Observable<any> {
  //   const model = {
  //     refreshToken: this.getRefreshToken(),
  //     username: this.getUsername()
  //   }

  //   return this.httpClient.post(ApiRouteConstants.TOKENS_CREATE_REFRESH_TOKEN, model);
  // }

  // isTokenExpired() {
  //   const accessToken = localStorage.getItem(GlobalConstants.ACCESS_TOKEN);
  //   if (accessToken) {
  //     return this.jwtHelper.isTokenExpired(accessToken);
  //   }

  //   return false;
  // }

  // tokenExists(): boolean {
  //   const accessToken = localStorage.getItem(GlobalConstants.ACCESS_TOKEN);
  //   const refreshToken = localStorage.getItem(GlobalConstants.REFRESH_TOKEN);

  //   if (accessToken && refreshToken) {
  //     return true;
  //   }

  //   return false;
  // }
}
