import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'sd-logout',
  templateUrl: './logout.component.html',
  styleUrl: './logout.component.css'
})

export class LogoutComponent {
  constructor(private router: Router, private authService: AuthService) { 
    this.authService.logout();
    this.router.navigate(['/']);
  }
}
