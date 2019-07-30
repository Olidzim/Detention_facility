import { Component, OnInit, Input,  EventEmitter, Output} from '@angular/core';
import { SmartEmployee } from '../models/smartemployee';
import { EmployeeService } from '../services/employee.service';
import { Employee } from '../models/employee';
import { SharedService } from '../services/shared.service';

@Component({
  selector: 'employee-detail',
  templateUrl: './employee-detail.component.html',
  styleUrls: ['./employee-detail.component.css']
})
export class EmployeeDetailComponent implements OnInit {
  @Output() toDeliveryChange = new EventEmitter<number>();
  @Input() employeeID: number;
  employee: Employee = new Employee();
  defaultemployee: Employee = new Employee();
  smartEmployee: SmartEmployee;
  foundEmployeeID: number;
  ifSearch: boolean = false;
  @Input() ifChange: boolean = false;
  constructor(private employeeService: EmployeeService, private sharedService: SharedService) { }

  ngOnInit() {
    console.log(this.employeeID)  
    this.foundEmployeeID= this.employee.employeeID;
    console.log(this.employeeID)    
    this.getEmployee();
    this.defaultemployee = this.employee;
    this.ifChange = this.sharedService.ifChange;    
  }

  ngOnChanges() {
  console.log(this.employeeID)
  if (this.sharedService.default == true) {
    console.log(this.defaultemployee)
    this.foundEmployeeID = this.employeeID;
    this.getEmployee();
    console.log(this.employee)
    this.sharedService.default =false;
    }
  }

  openSearch() {  
  this.ifSearch=true;
  }

  getEmployee() {
    console.log(this.employeeID);
    this.sharedService.default = false;    
    this.employeeService.getEmployeeByID(this.employeeID)
    .subscribe(
    res => this.employee = res);  
    this.defaultemployee = this.employee; 
    console.log(this.defaultemployee)
  }

  getEmployeeFromSearch(foundEmployee: SmartEmployee) {
  this.foundEmployeeID=foundEmployee.employeeID;
  //this.sharedService.default = false;
  this.ifSearch = false; 
  this.toDeliveryChange.emit(foundEmployee.employeeID);
  this.employeeService.getEmployeeByID(this.foundEmployeeID)
  .subscribe(
  res => this.employee = res);
  }
}
