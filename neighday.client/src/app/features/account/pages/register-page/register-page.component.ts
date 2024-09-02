import { Component, OnInit, inject } from '@angular/core';
import { Router } from '@angular/router';
import { RegisterFormComponent } from '../../components/register-form/register-form.component';
import { AuthService } from '../../services/auth.service';
import { first } from 'rxjs';

@Component({
  selector: 'app-register-page',
  standalone: true,
  imports: [RegisterFormComponent],
  templateUrl: './register-page.component.html',
  styleUrl: './register-page.component.css',
})
export class RegisterPageComponent implements OnInit {
  private readonly router = inject(Router);
  private readonly authService = inject(AuthService);

  ngOnInit() {
    this.authService
      .isAuthenticated()
      .pipe(first())
      .subscribe(() => this.router.navigate(['/']));
  }

  handleRegisterComplete() {
    this.router.navigate(['/login']);
  }
}
