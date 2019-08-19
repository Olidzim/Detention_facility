import { Component, OnInit } from '@angular/core';
import {SmartEmployee} from '../models/smartemployee';
import {Employee} from '../models/employee';
import {EmployeeService }  from '../services/employee.service';

@Component({
  selector: 'app-employee',
  templateUrl: './employee.component.html',
  styleUrls: ['./employee.component.css']
})
export class EmployeeComponent implements OnInit {
  
  employee: Employee = new Employee();
  employees : Employee[];
  tableMode: boolean = true;
  add: boolean = false;

  constructor(private employeeService: EmployeeService) { }

  ngOnInit() {
    this.loadsmartEmployees();  
  }


  loadsmartEmployees() {
    this.employeeService.getsmartEmployees()
    .subscribe((data: SmartEmployee[]) => this.employees = data);
  } 


  changeAdd(){
    this.add = !this.add;
  }


  editEmployee(e:Employee) {
    this.employee = e;
  }

  
  cancel() {
    this.employee = new Employee();
    this.tableMode = true;
  }

  
  createNewEmployee() {
    this.employeeService.createEmployee(this.employee)
    .subscribe(data => this.loadsmartEmployees());
       this.changeAdd();
  }


  saveChanges(){ 
      this.employeeService.updateEmployee(this.employee)
      .subscribe(data => this.loadsmartEmployees());
      this.cancel();    
    }
    
  
  delete(e: Employee) {
    this.employeeService.deleteEmployee(e.employeeID)
    .subscribe(data => this.loadsmartEmployees());
    }
    
}

