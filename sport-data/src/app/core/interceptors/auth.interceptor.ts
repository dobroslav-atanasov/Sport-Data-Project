import { HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { Injectable, inject } from "@angular/core";
import { Observable, catchError, throwError } from "rxjs";
import { environment } from "../../../environments/environment.development";
import { AuthService } from "../../auth/services/auth.service";

@Injectable()

export class AuthInterceptor implements HttpInterceptor {
  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const apiUrl = environment.apiUrl;
    req = req.clone({
      setHeaders: {
        'Content-Type': 'application/json'
      }
    });

    if (!req.url.startsWith(apiUrl)) {
      req = req.clone({
        url: `${apiUrl}${req.url}`
      });
    }

    const authService = inject(AuthService);
    const token = authService.getAccessToken();
    if (token) {
      req = req.clone({
        setHeaders: {
          Authorization: `Bearer ${token}`
        }
      })
    }

    return next.handle(req);
  }
}