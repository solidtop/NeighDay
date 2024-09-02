import { Component, OnInit, inject } from '@angular/core';
import { Router } from '@angular/router';
import { LoginFormComponent } from '../../components/login-form/login-form.component';
import { AuthService } from '../../services/auth.service';
import { first } from 'rxjs';

@Component({
  selector: 'app-login-page',
  standalone: true,
  imports: [LoginFormComponent],
  templateUrl: './login-page.component.html',
  styleUrl: './login-page.component.css',
})
export class LoginPageComponent implements OnInit {
  private readonly router = inject(Router);
  private readonly authService = inject(AuthService);

  ngOnInit() {
    this.authService
      .isAuthenticated()
      .pipe(first())
      .subscribe(() => this.router.navigate(['/']));
  }

  handleLoginComplete() {
    window.location.href = '/';
  }
}
