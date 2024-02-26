import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../services/auth.service';
import { NotificationsService } from '../../shared/services/notifications.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'sd-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})

export class LoginComponent implements OnInit {
  loginForm!: FormGroup;

  constructor(private formBuilder: FormBuilder,
    private authService: AuthService,
    private notificationsService: NotificationsService,
    private router: Router,
    private route: ActivatedRoute) { }

  get username() {
    return this.loginForm.controls['username'];
  }

  get password() {
    return this.loginForm.controls['password'];
  }

  ngOnInit(): void {
    this.createForm();
  }

  createForm() {
    this.loginForm = this.formBuilder.group({
      username: ['', [Validators.required]],
      password: ['', [Validators.required]]
    });
  }

  signInHandler() {
    this.authService
      .login(this.loginForm.value)
      .subscribe({
        next: () => {
          const backUrl = this.route.snapshot.queryParams['returnUrl'] || '';
          this.router.navigate([backUrl])
        },
        error: (err) => {
          this.notificationsService.showError(err.error.message);
        }
      })
  }
}
