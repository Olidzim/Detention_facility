import { Component, OnInit } from '@angular/core';
import { NgForm, FormsModule } from '@angular/forms';
import { LoginService } from './login.service';
import {HttpParams} from '@angular/common/http';
import {HttpClient} from "@angular/common/http"
import { Router } from '@angular/router';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  formModel={
    Login:'',
    Password:''
    };
    
  constructor(private service: LoginService, private http: HttpClient, private router: Router) { }

  ngOnInit() {
  }

  onSubmit(form: NgForm){

    const payload = new HttpParams()
    .set('grant_type', "password")
    .set('username', this.formModel.Login)
    .set('password', this.formModel.Password);
    
    this.http.post('http://localhost:58653/token', payload).subscribe((res:any) => {
      localStorage.setItem('token',res.access_token);    
    
      this.http.get('http://localhost:58653/api/account/getrole').subscribe((res:any) => {
        localStorage.setItem('role',res);
        this.router.navigateByUrl('/home');})
    })

    }
  }

