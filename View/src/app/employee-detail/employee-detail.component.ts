import { Component, OnInit, Input,  EventEmitter, Output} from '@angular/core';
import { Router} from '@angular/router';
import { SmartEmployee } from '../models/smartemployee';
import { EmployeeService } from '../services/employee.service';
import { Employee } from '../models/employee';
import { SharedService } from '../services/shared.service';
import { Alert } from 'selenium-webdriver';

@Component({
  selector: 'employee-detail',
  templateUrl: './employee-detail.component.html',
  styleUrls: ['./employee-detail.component.css']
})
export class EmployeeDetailComponent implements OnInit {

  @Input() employeeID: number;
  @Input() ifChange: boolean = false;

  @Output() toDeliveryChange = new EventEmitter<number>();
  @Output() toReleaseChange = new EventEmitter<number>();
  @Output() toDetentionChange = new EventEmitter<number>();
  @Output() toDeliveryAdd = new EventEmitter<number>();
  @Output() toReleaseAdd = new EventEmitter<number>();
  

  employee: Employee = new Employee();
  defaultemployee: Employee = new Employee();
  smartEmployee: SmartEmployee;
  foundEmployeeID: number;
  ifSearch: boolean = false;


  constructor(
    private employeeService: EmployeeService, 
    private sharedService: SharedService, 
    private router: Router
    ) { }


  ngOnInit() {   
    this.foundEmployeeID = this.employee.employeeID;
    this.getEmployee();
    this.defaultemployee = this.employee;    
    this.ifChange = this.sharedService.ifChange;
  }


  ngOnChanges() {
    if (this.sharedService.default == true) {
      this.foundEmployeeID = this.employeeID;
      this.getEmployee();
      this.sharedService.default = false;
    }
  }

  openSearch() {  
  this.ifSearch = true;
  }


  getEmployee() {  
    this.sharedService.default = false;    
    this.employeeService.getEmployeeByID(this.employeeID)
    .subscribe(
    res => this.employee = res);  
    this.defaultemployee = this.employee;    
  }


  getEmployeeFromSearch(foundEmployee: SmartEmployee) {
    this.foundEmployeeID = foundEmployee.employeeID;
    this.ifSearch = false;
    if (this.sharedService.ifDetention) {  
      this.toDetentionChange.emit(foundEmployee.employeeID);
    }
    else {  
      this.toDeliveryAdd.emit(foundEmployee.employeeID);
      this.toReleaseAdd.emit(foundEmployee.employeeID);
      this.toDeliveryChange.emit(foundEmployee.employeeID);
      this.toReleaseChange.emit(foundEmployee.employeeID);
    }  
    this.employeeService.getEmployeeByID(this.foundEmployeeID)
    .subscribe(
    res => this.employee = res);
  }
  
} 
