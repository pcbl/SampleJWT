import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../auth.service';
import { LoginDto } from '../model/loginDto';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  message:string;
  form;
  constructor(private fb: FormBuilder,
    private router: Router,
    private auth: AuthService) {
    this.form = fb.group({
      userName: ['', [Validators.required]],
      password: ['', Validators.required]
    });
  }
  ngOnInit() {
  }
  login() {
    if (this.form.valid) {
      var login = new LoginDto();
      login.Domain="GFT.COM";
      login.UserName=this.form.value.userName;
      login.Password=this.form.value.password;
      this.auth.login(login).then(data=>{
        if(!data)
        {
          this.router.navigate(["home"]);
        }
        else
        {
          this.message = data;
        }
      });
    }
  }


}
