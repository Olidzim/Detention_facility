import { Component, OnInit } from '@angular/core';
import { UserService } from '../services/user.service';
import { User } from '../models/user';
import {SharedService} from '../services/shared.service'
import { Router } from '@angular/router';
import {Roles} from '../registration/roles';

@Component({
  selector: 'app-user-detail',
  templateUrl: './user-detail.component.html',
  styleUrls: ['./user-detail.component.css']
})
export class UserDetailComponent implements OnInit {
  
  roles = Roles;
  user: User;

  constructor(

    private userService: UserService,
    private sharedService: SharedService,
    private router: Router

  ) { }

  ngOnInit() {

    this.getUser(this.sharedService.forUserDetailID)
  }

  getUser(id: number): void {

    this.userService.getUserByID(id)
    .subscribe(response =>     
    {this.user = response     
    });  
  }

  deleteCurrentDetainee()
  {   
    if (localStorage.getItem('login') == this.user.login) {
      alert("Нельзя удалить текущего пользователя")
    }
    else {
      this.userService.deleteUser(this.user.userID)
      .subscribe (data => { 
      this.router.navigateByUrl('/home/user');
      });
    }
  }
}
