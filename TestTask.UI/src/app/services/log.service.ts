import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { GetLogsResponse } from '../models/response/GetAllLogsResponse';
import { Observable } from 'rxjs';
import { environment } from '../environment/environment';

@Injectable({
  providedIn: 'root'
})
export class LogService {
  constructor(private http: HttpClient) { }

  
  public getAllLogs() : Observable<GetLogsResponse> {
    return this.http.get<GetLogsResponse>(`${environment.logApi}/all`);
  }

  public getLogs({number:pageNumber=0,number:PageSize=0}) : Observable<GetLogsResponse> {
    return this.http.get<GetLogsResponse>(`${environment.logApi}/paged?pageNumber=${pageNumber}&pageSize=${PageSize}`);
  }
}
