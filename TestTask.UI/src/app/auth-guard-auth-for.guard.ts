import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';

export const authGuardAuthForGuard: CanActivateFn = (route, state) => {
  const token = localStorage.getItem("jwtToken");
  const router = inject(Router);
  console.log(router);
  if(token){
    router.navigate(["/users"]);
    return false;
  }
  return true;
    
};
