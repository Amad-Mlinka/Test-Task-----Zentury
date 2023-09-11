import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';

export const logOffGuard: CanActivateFn = (route, state) => {
  const token = localStorage.removeItem("jwtToken");
  const router = inject(Router);
  router.navigate(["login"]);
  return false;
};
