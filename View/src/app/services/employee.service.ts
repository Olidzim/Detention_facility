import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Employee } from '../models/employee';
import { SmartEmployee } from '../models/smartemployee';
import { Observable, of } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};
@Injectable({ providedIn: 'root' })
export class EmployeeService {

    private employeeUrl = 'http://localhost:58653/api/employee'; 

    constructor(private http: HttpClient) { }

        getEmployeeByID(id: number){           
            return this.http.get(`${this.employeeUrl}/GetEmployee/${id}`);
        }

        createEmployee(employee: Employee) { 
          return this.http.post(`${this.employeeUrl}/InsertEmployee`, employee);
        }

        getsmartEmployees() {          
            return this.http.get(`${this.employeeUrl}/GetEmployees/`);    
        }

        updateEmployee(employee: Employee) {
            return this.http.put(`${this.employeeUrl}/UpdateEmployee/${employee.employeeID}`, employee);
        }

        deleteEmployee(id: number) {
            return this.http.delete(`${this.employeeUrl}/DeleteEmployee/${id}`);
        }

        searchEmployees(term: string): Observable<SmartEmployee[]> {
            if (!term.trim()) {
              return of([]);
            }
            return this.http.get<SmartEmployee[]>(`${this.employeeUrl}/GetEmploy?term=${term}`).pipe(            
              catchError(this.handleError<SmartEmployee[]>('searchEmployees', []))
            );
        }
        
        private handleError<T> (operation = 'operation', result?: T) {
        return (error: any): Observable<T> => {
            console.error(error);
            return of(result as T);
            };
        }  
    }