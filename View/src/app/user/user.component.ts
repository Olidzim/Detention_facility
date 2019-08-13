import { Component, OnInit } from '@angular/core';
import { UserService } from '../services/user.service';
import { User } from '../models/user';
import {SharedService} from '../services/shared.service'
import { Router } from '@angular/router';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {

  users: User[];

  constructor(
    
    private userService: UserService,
    private sharedService: SharedService,
    private router: Router

    ) { }

  ngOnInit() {
    this.loadsmartDetentions()
  }

  toUserDetail(u: User)
  {    
    this.sharedService.forUserDetailID = u.userID;    
    this.router.navigateByUrl('/home/user/user-detail/');
  }

  loadsmartDetentions() {
    console.log("NoDate")
    this.userService.getUsers()
    .subscribe((data: User[]) => this.users = data);
  }

}
