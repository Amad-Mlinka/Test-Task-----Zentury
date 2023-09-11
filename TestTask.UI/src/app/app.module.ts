import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { MatTableModule} from '@angular/material/table';
import { MatPaginatorModule} from '@angular/material/paginator';
import { SweetAlert2Module } from '@sweetalert2/ngx-sweetalert2';
import { MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import {MatSortModule} from '@angular/material/sort';
import {MatButtonModule} from '@angular/material/button';
import {NgxPaginationModule} from 'ngx-pagination';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { RegisterComponent } from './components/login-register/register/register.component';
import { UserListComponent } from './components/lists/user-list/user-list.component';
import { EditModalComponent } from './components/edit-modal/edit-modal.component';
import { LoginComponent } from './components/login-register/login/login.component';
import { AuthenticationInterceptor } from './services/intercepter';
import { LoginRegisterComponent } from './components/login-register/login-register.component';
import { LoginListComponent } from './components/lists/login-list/login-list.component';


@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    RegisterComponent,
    UserListComponent,
    EditModalComponent,
    LoginComponent,
    LoginRegisterComponent,
    LoginListComponent,

  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    MatTableModule,
    MatPaginatorModule,
    BrowserAnimationsModule,
    [SweetAlert2Module.forRoot()],
    MatDialogModule,
    MatFormFieldModule,
    ReactiveFormsModule,
    MatSortModule,
    MatButtonModule,
    NgxPaginationModule
  ],
  providers: [{
    provide:HTTP_INTERCEPTORS,
    useClass:AuthenticationInterceptor,
    multi:true
  }],
  bootstrap: [AppComponent]
})
export class AppModule { }
