import { Component } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Register } from '../../models/register';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { LoginComponent } from '../login/login.component';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
  hidePassword = true;
  registerForm = new FormGroup({
    username: new FormControl('', [Validators.required, Validators.nullValidator]),
    email: new FormControl('', [Validators.required, Validators.nullValidator, Validators.email]),
    password: new FormControl('', [Validators.required])
  });

  constructor(
    private authService: AuthService, 
    public dialogRef: MatDialogRef<RegisterComponent>,
    public dialog: MatDialog,
    private messageSnack: MatSnackBar
  ) {}

  get username() {
    return this.registerForm.get('username');
  }
  get email() {
    return this.registerForm.get('email');
  }

  get password() {
    return this.registerForm.get('password');
  }

  signUp(): void {
    let registerData : Register = {
      username: this.username?.value,
      email: this.email?.value,
      password: this.password?.value
    };

    if (!this.registerForm.invalid) {
      this.authService.signUpUser(registerData).subscribe({
        next: result => {
          console.log(result);
          this.showSnackBar(result);
          this.dialogRef.close();
        }
      })
    }
  }

  onLoginClick() {
    this.dialogRef.close();
    this.dialog.open(LoginComponent);
  }

  showSnackBar(message: string) {
    this.messageSnack.open(message, "Dismiss", {});
  } 
}
