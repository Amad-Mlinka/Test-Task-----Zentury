import { Component, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { Log } from 'src/app/models/Log';
import { LogService } from 'src/app/services/log.service';
import { GetLogsResponse } from 'src/app/models/response/GetLogsResponse';

@Component({
  selector: 'app-login-list',
  templateUrl: './login-list.component.html',
  styleUrls: ['./login-list.component.css']
})
export class LoginListComponent {
  users: Log[] = [];

  dataSource = new MatTableDataSource<Log>();
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
  }

  constructor(private logService: LogService) {}

  updateUsersList(){
    this.logService.getLogs()
    .subscribe(
      (result: GetLogsResponse)=>{
        console.log(result);
        this.dataSource.data = result.data;
    });   
  }


  ngOnInit() : void {
    this.updateUsersList()
  }  


  
}
