import { Component, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { Log } from 'src/app/models/Log';
import { LogService } from 'src/app/services/log.service';
import { GetLogsResponse } from 'src/app/models/response/GetAllLogsResponse';
import { MatSort } from '@angular/material/sort';

@Component({
  selector: 'app-login-list',
  templateUrl: './login-list.component.html',
  styleUrls: ['./login-list.component.css']
})
export class LoginListComponent {
  users: Log[] = [];

  dataSource = new MatTableDataSource<Log>();

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;


  constructor(private logService: LogService) {
    
  }

  updateUsersList(){
    this.logService.getAllLogs()
    .subscribe(
      (result: GetLogsResponse)=>{
        this.dataSource = new MatTableDataSource<Log>(result.data)
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
    });   
  }


  ngOnInit() : void {
    this.updateUsersList()
  }  


  
}
