import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { filter, map } from 'rxjs';

export const authGuard: CanActivateFn = () => {
  const authService = inject(AuthService);
  const router = inject(Router);

  return authService.user$.pipe(
    filter((user) => user !== undefined),
    map((user) => {
      if (!user) {
        router.navigateByUrl('/login');
        return false;
      }

      return true;
    })
  );
};
