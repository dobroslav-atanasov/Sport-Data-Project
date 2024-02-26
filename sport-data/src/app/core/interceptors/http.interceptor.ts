import { HTTP_INTERCEPTORS } from "@angular/common/http";
import { Provider } from "@angular/core";
import { ErrorInterceptor } from "./error.interceptor";
import { AuthInterceptor } from "./auth.interceptor";
import { TokenInterceptor } from "./token.interceptor";

export const HttpInterceptorProviders: Provider[] = [
  {
    provide: HTTP_INTERCEPTORS,
    useClass: ErrorInterceptor,
    multi: true
  },
  {
    provide: HTTP_INTERCEPTORS,
    useClass: AuthInterceptor,
    multi: true
  },
]