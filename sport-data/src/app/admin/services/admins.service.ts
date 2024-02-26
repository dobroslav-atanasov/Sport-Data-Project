import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, Observer } from 'rxjs';
import { ApiRouteConstants } from '../../core/constants/api-route-constants';

@Injectable({
  providedIn: 'root'
})
export class AdminsService {

  constructor(private httpClient: HttpClient) { }

  getUsers(): Observable<any> {
    return this.httpClient.get(ApiRouteConstants.USERS);
  }
}
