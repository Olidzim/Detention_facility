import { Component, OnInit } from '@angular/core';
import {Roles} from './roles';
import {User} from '../models/user';
import {UserService} from '../services/user.service';
import { Router } from "@angular/router";

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent implements OnInit {

  role;
  roles = Roles;
  user : User = new User;
  constructor( 

    private router: Router,
    private userService: UserService

  ) { }

  ngOnInit() {
  }
  addUser()
  {
    console.log(this.role)
    this.user.role = this.role
    this.userService.createUser(this.user)
    .subscribe(data=>this.router.navigateByUrl('/home/user'))    
  }
}
