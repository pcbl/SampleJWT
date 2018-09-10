import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { LoginDto } from './model/loginDto';
import { HttpClient } from '@angular/common/http';

//More info here: https://dzone.com/articles/implementing-guard-in-angular-5-app
@Injectable()
export class AuthService {

  constructor(private router: Router, private http: HttpClient) { }
  login(login: LoginDto): Promise<string> {
    return new Promise((resolve, reject) => {
      var url = 'http://localhost:50899/api/Security/Login';
      this.http.post<string>(url, login).subscribe(
        token => {
          localStorage.setItem("LoggedInUser", token);
          resolve('');
        },
        error => {
          resolve(error.error);
        } 
      );      
    });
  }

  sendToken(token: string) {
    localStorage.setItem("LoggedInUser", token)
  }
  getToken() {
    return localStorage.getItem("LoggedInUser")
  }
  isLoggedIn() {
    return this.getToken() !== null;
  }
  logout() {
    localStorage.removeItem("LoggedInUser");
    this.router.navigate(["login"]);
  }

}
