import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CoreModule } from './core/core.module';
import { AuthModule } from './auth/auth.module';
import { SharedModule } from './shared/shared.module';
import { ToastModule } from 'primeng/toast';
import { PageComponent } from './page/page.component';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { globalErrorInterceptor } from './shared/interceptors/global-error.interceptor';
import { authInterceptor } from './shared/interceptors/auth.interceptor';

@NgModule({
  declarations: [
    AppComponent,
    PageComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    CoreModule,
    SharedModule,
    AuthModule,
    ToastModule
  ],
  providers: [
    provideHttpClient(withInterceptors([globalErrorInterceptor, authInterceptor]))
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
