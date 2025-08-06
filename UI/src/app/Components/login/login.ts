import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { tap } from 'rxjs';
import { JwtHelperService } from '@auth0/angular-jwt';
import { AuthService } from '../../Services/Authorization/auth-service';
import { Token } from '@angular/compiler';

@Component({
  selector: 'app-login',
  standalone: false,
  templateUrl: './login.html',
  styleUrl: './login.css'
})
export class LoginComponent {
  username = '';
  password = '';
  role = ''; // default role
  errorMessage = '';
  isAdmin = false; // toggle between Admin/customer login
  helper = new JwtHelperService();
  constructor(private authService: AuthService, private router: Router, private http :HttpClient) {}
  onLogin() {
    
    this.authService.AdminLogin(this.username,this.password).subscribe({
      next: (response) => {
        console.log("Response ID: ",response.customerId);
        localStorage.setItem('token', response.token);
      const decodedToken = this.helper.decodeToken(response.token);
      console.log(decodedToken);
      localStorage.setItem('role', decodedToken.role); 
      localStorage.setItem('customerId', response.customerId);
      this.router.navigate(['/home']);
      },
      error: () => {
          this.authService.customerLogin(this.username,this.password).subscribe({
            next: (response) => {
            console.log("Response ID: ",response.customerId);
            localStorage.setItem('token', response.token);
            const decodedToken = this.helper.decodeToken(response.token);
            localStorage.setItem('role', decodedToken.role); 
            localStorage.setItem('customerId', response.customerId);
            this.router.navigate(['/home']);
            },
            error: () => {
               this.errorMessage = 'Invalid username or password';
            },
          });
        },
    });
  }
  showSignup = false;
  toggleSignup() {
    this.showSignup = !this.showSignup;
  }
}


