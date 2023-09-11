import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { User } from '../models/User';
import { Observable } from 'rxjs';
import { environment } from '../environment/environment';
import { GetUsersResponse } from '../models/response/GetUsersResponse';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  constructor(private http: HttpClient) { }

  public getUsers() : Observable<GetUsersResponse> {
    return this.http.get<GetUsersResponse>(`${environment.usersApi}`);
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
      `$${environment.usersApi}/${user.id}`
      );
  }
}
