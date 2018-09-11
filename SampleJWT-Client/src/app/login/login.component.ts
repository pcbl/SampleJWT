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
  public working:boolean = false;
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
      this.working = true;
      this.auth.login(login).then(data=>{
        this.working = false;
        if(!data)
        {
          this.router.navigate(["home"]);
        }
        else
        {
          this.message = data;
        }
      }).catch(ex=>{
        this.working = false;
        this.message = ex.message;;
      });
    }
  }


}
