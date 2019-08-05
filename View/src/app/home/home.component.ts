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
  show: number = 1;
  constructor(private calendar: NgbCalendar, private router: Router, private sharedService: SharedService) { }

  ngOnInit() {
   
  }

  ngOnChanges() {   
   
   }
   onDateSelected()
   {
   console.log(this.model)
   //this.sharedService.date = 
   this.sharedService.changeMessage(new Date(this.model.year, this.model.month - 1, this.model.day))
   }

  tablePanel()
  {
    this.show = 1;
  }
 searchPanel ()
  {
    this.show = 2;
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

}
