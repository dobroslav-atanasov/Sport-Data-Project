import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomeComponent } from './home/home.component';
import { HeaderComponent } from './header/header.component';
import { FooterComponent } from './footer/footer.component';
import { RouterModule } from '@angular/router';
import { MenubarModule } from 'primeng/menubar';
import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';
import { NotFoundComponent } from './not-found/not-found.component';
import { CardModule } from 'primeng/card';
import { HttpInterceptorProviders } from './interceptors/http.interceptor';
import { SidebarComponent } from './sidebar/sidebar.component';
import { SidebarModule } from 'primeng/sidebar';

@NgModule({
  declarations: [
    HomeComponent,
    HeaderComponent,
    FooterComponent,
    NotFoundComponent,
    SidebarComponent
  ],
  imports: [
    CommonModule,
    RouterModule,
    MenubarModule,
    ButtonModule,
    InputTextModule,
    CardModule,
    SidebarModule
  ],
  exports: [
    HomeComponent,
    HeaderComponent,
    FooterComponent,
    SidebarComponent,
    NotFoundComponent
  ],
  providers: [
    HttpInterceptorProviders,
  ]
})

export class CoreModule { }