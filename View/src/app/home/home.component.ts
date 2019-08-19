import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import {NgbDateStruct, NgbCalendar, NgbDate} from '@ng-bootstrap/ng-bootstrap';
import {SharedService} from '../services/shared.service';


@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  //date: {year: number, month: number, day: number};
  isSearch: boolean;
  viewDate;
  constructor(private calendar: NgbCalendar, private router: Router, private sharedService: SharedService) { }

  ngOnInit() {
    this.isSearch = false;
  }

  isAdmin() {
    return localStorage.getItem('role') == 'Admin';
  }

  ngOnChanges() {   
   
   }
   onDateSelected() {
   this.viewDate = new Date(this.model.year, this.model.month - 1, this.model.day); 
   this.sharedService.changeMessage(this.viewDate)   
   }
   
  onLogout() {
    localStorage.removeItem('token');
    this.router.navigate(['/login']);
  }

  model: NgbDate;
  date: NgbDate;

  selectToday() {
    this.model = this.calendar.getToday();
  }

  toDefaultDate()
  {
    this.model = undefined;
    this.sharedService.changeMessage(undefined);
    this.viewDate = undefined;
  }

}
