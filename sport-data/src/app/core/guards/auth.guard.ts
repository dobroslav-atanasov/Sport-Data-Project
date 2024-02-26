import { CanActivateFn, Router } from '@angular/router';
import { inject } from '@angular/core';
import { AuthService } from '../../auth/services/auth.service';

export const authGuard: CanActivateFn = (route, state) => {
  const authService = inject(AuthService);
  const router = inject(Router);

  const isLoggedIn = authService.isLoggedIn();
  if (isLoggedIn) {
    return true;
  }

  return router.navigate(['/auth/login'], { queryParams: { returnUrl: state.url } });
};