import { inject } from '@angular/core';
import { CanActivateFn,CanDeactivateFn, Router } from '@angular/router';

export const loginRegisterAuthRequiredGuard: CanActivateFn = (route, state) => {
  const token = localStorage.getItem("jwtToken");
  const router = inject(Router);
  if(token)
    return true;
  router.navigate(["login"]);
  return false;
};

export const loginRegisterAuthForbidenGuard: CanActivateFn = (route, state) => {
  const token = localStorage.getItem("jwtToken");
  console.log(token)
  const router = inject(Router);
  console.log("forbiden guard")
  if(!token)
    return true;
  router.navigate(["users"]);
  return false;
};