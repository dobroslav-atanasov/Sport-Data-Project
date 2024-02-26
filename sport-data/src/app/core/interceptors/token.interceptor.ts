import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { Injectable, inject } from "@angular/core";
import { Observable } from "rxjs";
import { AuthService } from "../../auth/services/auth.service";

@Injectable()

export class TokenInterceptor implements HttpInterceptor {
  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

    const authService = inject(AuthService);
    if (authService.isLoggedIn() && authService.isTokenExpired()) {
      const refreshToken = authService.getRefreshToken();
      const username = authService.getUsername();

      if (refreshToken && username) {
        authService.createRefreshToken()
        .subscribe({
          next: (res) => {
            console.log(res)
          }, 
          error: (err) => {
            console.log(err);
          }
        })
      }
    }

    return next.handle(req);
  }
}