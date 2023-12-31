import { Component, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { User } from 'src/app/models/User';
import { UserService } from 'src/app/services/user-service';
import { EditModalComponent } from '../../edit-modal/edit-modal.component';
import { GetUsersResponse } from 'src/app/models/response/GetUsersResponse';
import { MatSort } from '@angular/material/sort';
import { GetAllUsersResponse } from 'src/app/models/response/GetAllUsersResponse';


@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.css'],
})
export class UserListComponent {
  users: User[] = [];
  editedUser?: User;

  dataSource = new MatTableDataSource<User>();

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;


  constructor(private userService: UserService,private dialog: MatDialog) {
    
  }

  updateUsersList(){
    this.userService.getAllUsers()
    .subscribe(
      (result: GetAllUsersResponse)=>{
        this.dataSource = new MatTableDataSource<User>(result.data)
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
    });   
  }


  ngOnInit() : void {
    this.updateUsersList()
  }  

  initNewUser (){
    this.editedUser = new User();
  }

  editUser(user: User){
    this.editedUser= user;
  }
  
  deleteUser(user: User){
    this.userService.deleteUser(user).subscribe({
      next:()=>{
        this.updateUsersList();
      }
    });
  }

  openEditUserModal(user: User): void {
    const dialogRef = this.dialog.open(EditModalComponent, {
      width: '400px',
      data: { user }, // Pass the user to the modal
    });
  
    dialogRef.afterClosed().subscribe((result: User) => {
      if (result) {
        console.log(result)
        this.userService.updateUser(result).subscribe({
          next:()=>{
            this.updateUsersList();
          }
        });
      }
    });
  }

  
  openAddUserModal(): void {
    const dialogRef = this.dialog.open(EditModalComponent, {
      width: '400px',
      data: {  newUser:true }, // Pass the user to the modal
    });
  
    dialogRef.afterClosed().subscribe((result: User) => {
      if (result) {
        console.log(result)
        this.userService.createUser(result).subscribe({
          next:()=>{
            this.updateUsersList();
          }
        });
      }
    });
  }
  
}
