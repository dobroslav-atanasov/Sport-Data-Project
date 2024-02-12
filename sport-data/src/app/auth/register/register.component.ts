import { Component } from '@angular/core';
import { FormBuilder } from '@angular/forms';

@Component({
  selector: 'sd-register',
  templateUrl: './register.component.html',
  styleUrl: './register.component.css',
})

export class RegisterComponent {
  constructor(private formBuilder: FormBuilder) { }

  registerForm = this.formBuilder.group({
    username: [''],
    email: [''],
    password: [''],
    confirmPassword: ['']
  })
}