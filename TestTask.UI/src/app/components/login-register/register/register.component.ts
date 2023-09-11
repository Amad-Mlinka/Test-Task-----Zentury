import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Register } from 'src/app/models/Register';
import { AuthenticationService } from 'src/app/services/authentication.service';


@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {
  userForm: FormGroup;
  registerDTO = new Register();
  userExists : boolean = false;
  constructor(
    private formBuilder: FormBuilder,private authService : AuthenticationService
  ) {
    this.userForm = this.formBuilder.group({
      username: [
        "",
        [Validators.required]
      ],
      email: [
        "",
        [
          Validators.required, 
          Validators.pattern("^[a-z0-9._%+-]+@[a-z0-9.-]+\\.[a-z]{2,4}$")
        ]
      ],
      password: [
        "",
        [
          Validators.required,
          Validators.pattern("^(?=.*[A-Z])(?=.*\\d).{6,}$")
        ]
      ],
    });
  }

  saveChanges(): void {
    if (this.userForm.valid) {
      this.register(this.userForm.value);

    }
  }

  register(registerDTO: Register) {
    this.authService.register(registerDTO).subscribe(
      response=>{
        if (response.success===true) {
            this.userForm.reset();
            this.userExists=false;

        }else if(response.message ==="User already exists"){
          this.userExists=true;
        }
      }
    )
  }
  // Helper function to check if a form control has errors
  hasError(controlName: string, errorName: string): boolean {
    const control = this.userForm.get(controlName);
    return !!control && control.hasError(errorName);
  }

}
