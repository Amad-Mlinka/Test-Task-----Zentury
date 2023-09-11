import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { UserListComponent } from './components/lists/user-list/user-list.component';
import { LoginComponent } from './components/login-register/login/login.component';
import { LoginRegisterComponent } from './components/login-register/login-register.component';
import { authGuardAuthReqGuard } from './auth-guard-auth-req.guard';
import { authGuardAuthForGuard } from './auth-guard-auth-for.guard';
import { LoginListComponent } from './components/lists/login-list/login-list.component';
import { logOffGuard } from './log-off.guard';
const routes: Routes = [
  {
    path:"",
    component:UserListComponent,
    canActivate:[authGuardAuthReqGuard]

  },
  {
    path:"login",
    component:LoginRegisterComponent,
    canActivate:[authGuardAuthForGuard]

  },
  {
    path:"register",
    component:LoginRegisterComponent,
    canActivate:[authGuardAuthForGuard]

  },
  {
    path:"users",
    component:UserListComponent,
    canActivate:[authGuardAuthReqGuard]
  },
  {
    path:"logins",
    component:LoginListComponent,
    canActivate:[authGuardAuthReqGuard]
  },
  {
    path:"logout",
    component:LoginRegisterComponent,
    canActivate:[logOffGuard]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
