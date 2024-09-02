import { Component, EventEmitter, Output, inject } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { AuthService } from '../../services/auth.service';
import { LoginRequest } from '../../models/login-request.model';
import { first, pipe } from 'rxjs';

@Component({
  selector: 'app-login-form',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './login-form.component.html',
  styleUrl: './login-form.component.css',
})
export class LoginFormComponent {
  private readonly formBuilder = inject(FormBuilder);
  private readonly authService = inject(AuthService);
  errorMessage = '';

  readonly form: FormGroup = this.formBuilder.group({
    username: ['', Validators.required],
    password: ['', Validators.required],
  });

  @Output() loginCompleteEvent = new EventEmitter<void>();

  handleLogin(ev: Event) {
    ev.preventDefault();
    this.errorMessage = '';

    const request = this.form.value as LoginRequest;

    this.authService
      .login(request)
      .pipe(first())
      .subscribe({
        error: () => (this.errorMessage = 'Incorrect username or password'),
        complete: () => this.loginCompleteEvent.emit(),
      });
  }
}
