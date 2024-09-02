import { Component, EventEmitter, Output, inject } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { AuthService } from '../../services/auth.service';
import { RegisterRequest } from '../../models/register-request.model';
import { ErrorMessage } from '../../models/error-message.model';
import { first } from 'rxjs';

@Component({
  selector: 'app-register-form',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './register-form.component.html',
  styleUrl: './register-form.component.css',
})
export class RegisterFormComponent {
  private readonly formBuilder = inject(FormBuilder);
  private readonly authService = inject(AuthService);
  errors: ErrorMessage[] = [];

  readonly form: FormGroup = this.formBuilder.group({
    username: ['', Validators.required],
    email: ['', Validators.email],
    password: ['', Validators.required],
  });

  @Output() registerCompleteEvent = new EventEmitter<void>();

  handleRegister(ev: Event) {
    ev.preventDefault();
    this.errors = [];

    const request = this.form.value as RegisterRequest;

    this.authService
      .register(request)
      .pipe(first())
      .subscribe({
        error: (response) => (this.errors = response.error.errors),
        complete: () => this.registerCompleteEvent.emit(),
      });
  }
}
