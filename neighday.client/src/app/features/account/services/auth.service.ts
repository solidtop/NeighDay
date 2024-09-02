import { Injectable, WritableSignal, inject, signal } from '@angular/core';
import { ApiService } from '../../../shared/services/api.service';
import { RegisterRequest } from '../models/register-request.model';
import { LoginRequest } from '../models/login-request.model';
import { UserDetails } from '../models/user-details.model';
import { BehaviorSubject, catchError, filter, first, map, takeWhile, tap, throwError } from 'rxjs';

type UserState = UserDetails | null | undefined;

@Injectable({
  providedIn: 'root',
})
export class AuthService extends ApiService {
  private readonly authUrl = `${this.apiUrl}/api/auth`;
  private readonly userUrl = `${this.apiUrl}/api/user`;
  readonly user$ = new BehaviorSubject<UserState>(undefined);

  register(request: RegisterRequest) {
    return this.http.post<void>(`${this.authUrl}/register`, JSON.stringify(request), {
      headers: this.headers,
      withCredentials: true,
    });
  }

  login(request: LoginRequest) {
    return this.http.post<void>(`${this.authUrl}/login`, JSON.stringify(request), {
      headers: this.headers,
      withCredentials: true,
    });
  }

  logout() {
    return this.http
      .post(`${this.authUrl}/logout`, null, {
        withCredentials: true,
      })
      .pipe(tap(() => this.user$.next(null)));
  }

  getUser() {
    return this.http
      .get<UserDetails>(this.userUrl, {
        headers: this.headers,
        withCredentials: true,
      })
      .pipe(tap((response) => this.user$.next(response)));
  }

  isLoading() {
    return this.user$.pipe(
      filter((user) => user === undefined),
      map((user) => !!user)
    );
  }

  isAuthenticated() {
    return this.user$.pipe(
      filter((user) => user !== undefined),
      map((user) => !!user)
    );
  }
}
