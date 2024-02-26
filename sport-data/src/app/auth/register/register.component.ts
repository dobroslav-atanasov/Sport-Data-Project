import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { passwordLowerCaseValidator, passwordMatchValidator, passwordNonAlphanumericValidator, passwordUpperCaseValidator } from '../../shared/validators/validators';
import { AuthService } from '../services/auth.service';
import { Router } from '@angular/router';
import { NotificationsService } from '../../shared/services/notifications.service';
import { IRegistration } from '../interfaces/registration';

@Component({
  selector: 'sd-register',
  templateUrl: './register.component.html',
  styleUrl: './register.component.css',
})

export class RegisterComponent {
  registerForm: FormGroup;

  constructor(private formBuilder: FormBuilder,
    private authService: AuthService,
    private router: Router,
    private notificationsService: NotificationsService
  ) {
    this.registerForm = this.formBuilder.group({
      username: ['', [Validators.required]],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6), passwordNonAlphanumericValidator, passwordUpperCaseValidator, passwordLowerCaseValidator]],
      confirmPassword: ['', [Validators.required]]
    }, {
      validators: passwordMatchValidator
    })
  }

  get username() {
    return this.registerForm.controls['username'];
  }

  get email() {
    return this.registerForm.controls['email'];
  }

  get password() {
    return this.registerForm.controls['password'];
  }

  get confirmPassword() {
    return this.registerForm.controls['confirmPassword'];
  }

  signUpHandler() {
    this.authService
      .register(this.registerForm.value as IRegistration)
      .subscribe({
        next: (res) => {
          this.notificationsService.showSuccess(res.message);
          this.router.navigate(['/auth/login']);
        },
        error: (err) => {
          this.notificationsService.showError(err.error.message);
        }
      })
  }
}