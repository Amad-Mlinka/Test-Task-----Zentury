import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { User } from '../models/User';
import { Observable } from 'rxjs';
import { environment } from '../environment/environment';
import { GetAllUsersResponse } from '../models/response/GetAllUsersResponse';
import { GetUsersResponse } from '../models/response/GetUsersResponse';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  constructor(private http: HttpClient) { }

  public getAllUsers() : Observable<GetAllUsersResponse> {
    return this.http.get<GetAllUsersResponse>(`${environment.usersApi}/all`);
  }

  public getUsers(pageNumber:number=0,pageSize:number=10) : Observable<GetUsersResponse> {
    return this.http.get<GetUsersResponse>(`${environment.usersApi}?pageNumber=${pageNumber}&pageSize=${pageSize}`);
  }

  public updateUser(user: User) : Observable<User[]> {
    return this.http.put<User[]>(
      `${environment.usersApi}`,
      user
      );
  }

  public createUser(user: User) : Observable<User> {
     let x =this.http.post<User>(
      `${environment.usersApi}`,
      user
      );
      return x;
  }

  public deleteUser(user: User) : Observable<User[]> {
    return this.http.delete<User[]>(
      `${environment.usersApi}/${user.id}`
      );
  }
}
