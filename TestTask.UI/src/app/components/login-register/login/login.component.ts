import { Component } from '@angular/core';
import { Login } from 'src/app/models/Login';
import { JwtAuth } from 'src/app/models/jwtAuth';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  userForm:FormGroup;
  loginDTO = new Login();
  jwtDTO = new JwtAuth();
  badLogin:boolean=false;
  constructor(
    private formBuilder: FormBuilder,private authService : AuthenticationService,private router : Router
  ) {
    this.userForm = this.formBuilder.group({
      username: [
        ""
      ],
      password: [
        "",
      ],
    });
  }
  saveChanges(): void {
      this.login(this.userForm.value);
  }

  login(loginDTO : Login){
    console.log("Login")
    this.authService.login(loginDTO).subscribe(jwtDTO =>{
      console.log(jwtDTO)
      if(jwtDTO.success){
        localStorage.setItem("jwtToken",jwtDTO.token);
        this.authService.setAuthentification();
        this.router.navigate(['/users']);
        this.badLogin=false
  
      }else{
        if(jwtDTO.message==="That username doesn't exist" || jwtDTO.message==="Wrong password"){
          this.badLogin=true;
        }
      }
    })
  }
  hasError(){
    return this.badLogin;
  }
}
