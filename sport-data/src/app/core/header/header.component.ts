import { Component, OnInit } from '@angular/core';
import { MenuItem } from 'primeng/api';
import { AuthService } from '../../auth/services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'sd-header',
  templateUrl: './header.component.html',
  styleUrl: './header.component.css',
})
export class HeaderComponent implements OnInit {

  userItems: MenuItem[] | undefined;
  items: MenuItem[] | undefined;

  constructor(private authService: AuthService,
    private router: Router) {
  }

  get isLoggedIn() {
    return this.authService.isLoggedIn();
  }

  ngOnInit(): void {
    this.items = [
      {
        label: 'Home',
        routerLink: '/',
      },
      {
        label: 'Sign up',
        routerLink: 'auth/register',
      },
      {
        label: 'Sign in',
        routerLink: 'auth/login'
      },
    ]

    this.userItems = [
      {
        label: 'Home',
        routerLink: '/',
      },
      {
        label: 'Profile',
        routerLink: 'auth/profile',
      },
      {
        label: 'Account',
        routerLink: 'auth/account',
      },
      {
        label: 'Users',
        routerLink: 'admin/users'
      }
    ]
  }
}
