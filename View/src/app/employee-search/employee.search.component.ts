import { Component, OnInit, Output, Input, EventEmitter, Inject } from '@angular/core';
import {MatDialog, MatDialogRef, MAT_DIALOG_DATA} from '@angular/material/dialog';
import { Observable, Subject } from 'rxjs';
import { SmartEmployee } from '../models/smartemployee';
import { EmployeeService }  from '../services/employee.service';
import {
   debounceTime, distinctUntilChanged, switchMap
 } from 'rxjs/operators';

@Component({
  selector: 'employee-search',
  templateUrl: './employee.search.component.html',
  styleUrls: ['./employee.search.component.css']
})
export class EmployeeSearchComponent implements OnInit {
  k: number = 0;
  employees$: Observable<SmartEmployee[]>;
  @Input() employeeFullNameParent: string;
  @Output() employeeForParent: SmartEmployee;
  @Output() toEmployeeDetail = new EventEmitter<SmartEmployee>();
  private searchTerms = new Subject<string>();
 
  constructor(private employeeService: EmployeeService   
    ) {}

  ngOnInit(): void {    
    this.employees$ = this.searchTerms.pipe(
    debounceTime(300),   
    distinctUntilChanged(),  
    switchMap((term: string) => this.employeeService.searchEmployees(term)),
    );
  }

  search(term: string): void {
    this.searchTerms.next(term);
  } 

  chooseEmployee(foundEmployee: SmartEmployee){
  this.employeeForParent = foundEmployee;
  this.k = this.employeeForParent.employeeID;
  this.toEmployeeDetail.emit(this.employeeForParent);
  this.employeeFullNameParent = this.employeeForParent.fullName;
  }
}