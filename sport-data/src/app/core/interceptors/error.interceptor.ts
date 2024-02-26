import { HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { Injectable, inject } from "@angular/core";
import { Observable, catchError, from, switchMap, throwError } from "rxjs";
import { AuthService } from "../../auth/services/auth.service";
import { IToken } from "../../auth/interfaces/token";
import { Router } from "@angular/router";

@Injectable()

export class ErrorInterceptor implements HttpInterceptor {

  constructor(private authService: AuthService, private router: Router) { }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(req)
      .pipe(catchError((error) => {
        if (error instanceof HttpErrorResponse) {
          if (error?.status === 401) {
            return this.refreshToken(req, next);
          } else {
            return throwError(() => error);
          }
        } else {
          return throwError(() => error);
        }
      }));
  }

  private refreshToken(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return from(this.authService.createRefreshToken())
      .pipe(
        switchMap((res: IToken) => {
          this.authService.setTokens(res);
          request = request.clone({
            setHeaders: {
              Authorization: `Bearer ${res.accessToken}`
            }
          });

          return next.handle(request);
        }),
        catchError((err) => {
          if (err.status == 403) {
            this.router.navigate(['/auth/logout']);
          }

          return throwError(() => err);
        })
      )
  }
}