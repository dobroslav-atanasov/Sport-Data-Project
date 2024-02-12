import { Component, OnInit } from '@angular/core';
import { MenuItem } from 'primeng/api';
import { Menubar } from 'primeng/menubar';

@Component({
  selector: 'sd-header',
  templateUrl: './header.component.html',
  styleUrl: './header.component.css',
})
export class HeaderComponent implements OnInit {

  items: MenuItem[] | undefined;

  ngOnInit(): void {
    this.items = [
      {
        label: 'Home',
        routerLink: '/'
      },
      {
        label: 'Register',
        routerLink: 'auth/register'
      },
      {
        label: 'Login',
        routerLink: 'auth/login'
      }
    ]
  }
}
