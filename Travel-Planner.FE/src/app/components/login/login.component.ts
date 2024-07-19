import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { Login } from '../../models/login';
import { User } from '../../models/user';
import { AuthService } from '../../services/auth.service';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { RegisterComponent } from '../register/register.component';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  hidePass = true;
  loginForm = new FormGroup({
    email: new FormControl('', [Validators.required, Validators.nullValidator, Validators.email]),
    password: new FormControl('', [Validators.required])
  });

  constructor(
    public dialog: MatDialog,
    public dialogRef: MatDialogRef<LoginComponent>,
    private authService: AuthService, 
    private router: Router,
    private messageSnack: MatSnackBar) {}

  get email() {
    return this.loginForm.get('email');
  }

  get password() {
    return this.loginForm.get('password');
  }

  login(): void {
    let loginData : Login = {
      email: this.email?.value,
      password: this.password?.value
    };
    console.log(loginData);
    
    if(this.isLoginDataValid()) {
      this.authService.logInUser(loginData).subscribe({
        next: (user: User) => {
          console.log(user);
          this.authService.setConnectedUser(user);
          this.authService.setToken(user.token);
          this.router.navigate(['/']);
        },
        error: (err: Error) => {
          this.showError(err);
        }
      });
    }
  }

  isLoginDataValid(): boolean {
    if(this.loginForm.invalid){
      return false;
    }
    return true;
  }

  onSignUpClick() {
    this.dialogRef.close();
    const signUpDialog = this.dialog.open(RegisterComponent);
    signUpDialog.afterClosed().subscribe(result => {
      if (result) {
        alert("registration successful");
      }
    })
  }

  showSnackBar(message: string) {
    this.messageSnack.open(message, "Dismiss", {});
  } 

  showError(error: Error): void {
    if(!error || !error.message){
      this.showSnackBar('Something went wrong, please try again later!');
    }
    else {
      this.showSnackBar(error.message);
    }
  }
}
