import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

import { User } from 'src/app/models/User';

@Component({
  selector: 'app-edit-modal',
  templateUrl: './edit-modal.component.html',
  styleUrls: ['./edit-modal.component.css'],
})
export class EditModalComponent {
  userForm: FormGroup;
  editedUser: User = new User();
  newUser?: boolean = false;

  constructor(
    public dialogRef: MatDialogRef<EditModalComponent>,
    @Inject(MAT_DIALOG_DATA) public data: { user?: User, newUser?: boolean },
    private formBuilder: FormBuilder
  ) {
    if (!data.newUser && data.user) {
      this.editedUser = { ...data.user };
    } else {
      this.newUser = data.newUser;
    }

    this.userForm = this.formBuilder.group({
      id: [
        this.editedUser.id
      ],
      username: [
        this.editedUser.username,
        [Validators.required]
      ],
      email: [
        this.editedUser.email,
        [
          Validators.required, 
          Validators.pattern("^[a-z0-9._%+-]+@[a-z0-9.-]+\\.[a-z]{2,4}$")
        ]
      ],
      password: [
        this.editedUser.password,
        [
          Validators.required,
          Validators.pattern("^(?=.*[A-Z])(?=.*\\d).{6,}$")
        ]
      ],
    });
  }


  saveChanges(): void {
    if (this.userForm.valid) {
      this.dialogRef.close(this.userForm.value);
    }
  }

  cancel(): void {
    this.dialogRef.close();
  }

  // Helper function to check if a form control has errors
  hasError(controlName: string, errorName: string): boolean {
    const control = this.userForm.get(controlName);
    return !!control && control.hasError(errorName);
  }
}