import { Component, OnInit, Output, EventEmitter } from '@angular/core';
 
import { Observable, Subject } from 'rxjs';
import { DetaineeService }  from '../services/detainee.service';
import {
   debounceTime, distinctUntilChanged, switchMap
 } from 'rxjs/operators';

@Component({
  selector: 'app-detainee-search',
  templateUrl: './detainee-search.component.html',
  styleUrls: ['./detainee-search.component.css']
})
export class DetaineeSearchComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

}
