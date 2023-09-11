import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Login } from '../models/Login';
import { Register } from '../models/Register';
import { Observable } from 'rxjs';
import { environment } from '../environment/environment';
import { JwtAuth } from '../models/jwtAuth';
import { RegisterResponse } from '../models/response/RegisterResponse';
import { LoginResponse } from '../models/response/LoginResponse';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
  private isAuthenticated : boolean = false;
  registerURL : string = `${environment.authApi}/register`;
  loginURL : string = `${environment.authApi}/login`
  constructor(private http : HttpClient) { }


  public register(user : Register) : Observable <RegisterResponse>{
    return this.http.post<RegisterResponse>(`${this.registerURL}`,user)
  }

  public login(user : Login) : Observable <LoginResponse>{
    this.isAuthenticated = true;
    return this.http.post<LoginResponse>(`${this.loginURL}`,user)
  }

  public getAuthentication(){
    return (localStorage.getItem("jwtToken")!= null)
  }

}
