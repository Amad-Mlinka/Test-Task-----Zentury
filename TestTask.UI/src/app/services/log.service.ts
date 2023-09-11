import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { GetLogsResponse } from '../models/response/GetLogsResponse';
import { Observable } from 'rxjs';
import { environment } from '../environment/environment';

@Injectable({
  providedIn: 'root'
})
export class LogService {
  constructor(private http: HttpClient) { }

  
  public getLogs() : Observable<GetLogsResponse> {
    return this.http.get<GetLogsResponse>(`${environment.logApi}`);
  }

}
