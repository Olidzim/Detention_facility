import { Injectable } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import {HttpClient, HttpHeaders} from "@angular/common/http"
@Injectable({
  providedIn: 'root'
})
export class LoginService {

  constructor(private fb:FormBuilder, private http: HttpClient) { }
  readonly BaseURI = 'http://localhost:58653/api';
  formModel = this.fb.group({
    Login: ['',Validators.required],
    Password: [''],
    Email: [''],
    Role: [''],
  });

  register ()
  {
var body = {
Login: this.formModel.value.Login,
Password: this.formModel.value.Password,
Email: this.formModel.value.Email,
Role: this.formModel.value.Role,
}
return this.http.post(this.BaseURI+'/Account/RegisterUser', body, {
  observe: 'response'
});
  }
  login(formData){
    return this.http.post('http://localhost:58653/token', formData);
  }
roleMatch(allowedRoles): boolean {
  var isMatch = false;
  var userRole = localStorage.getItem('role');
  allowedRoles.forEach(element => {
    if (userRole == element) {
      isMatch = true;
      return false;
    }
  });
  return isMatch;}
}