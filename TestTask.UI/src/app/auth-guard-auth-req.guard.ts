import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';

export const authGuardAuthReqGuard: CanActivateFn = (route, state) => {
  const token = localStorage.getItem("jwtToken");
  const router = inject(Router);
  if(token)
    return true;
  router.navigate(["login"]);
  return false;
};
