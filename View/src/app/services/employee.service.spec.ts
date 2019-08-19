import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { NO_ERRORS_SCHEMA, DebugElement } from '@angular/core';

import { TestBed } from '@angular/core/testing';
import { HttpClient, HttpResponse, HttpErrorResponse } from '@angular/common/http';

import { asyncData, asyncError } from '../../testing/async-observable-helpers';

import { SmartEmployee  } from '../models/smartemployee';

import { EmployeeService } from './employee.service';
import { Employee } from '../models/employee';

describe ('employeeService (with spies)', () => {
  let httpClientSpy: { get: jasmine.Spy };
  let httpClientSpyPost: { post: jasmine.Spy };
  let employeeService: EmployeeService;

  beforeEach(() => {
   
    httpClientSpy = jasmine.createSpyObj('HttpClient', ['get'] );
    httpClientSpyPost = jasmine.createSpyObj('HttpClient', ['post'] );
    employeeService = new EmployeeService(<any> httpClientSpy);
  });

  it('should return expected expectedSmartemployees (HttpClient called once)', () => {
    const expectedEmployees: Employee[] = [
        { employeeID: 1, firstName: 'A' }, 
        { employeeID: 2, firstName: 'B' }];


    httpClientSpy.get.and.returnValue(asyncData(expectedEmployees));

    employeeService.getsmartEmployees().subscribe(
      smartDetentions => expect(smartDetentions).toEqual(expectedEmployees, 'expectedSmartemployees'),
      fail
    );
    expect(httpClientSpy.get.calls.count()).toBe(1, 'one call');
  });
});

describe('employeeService (with mocks)', () => {
    let httpClient: HttpClient;
    let httpTestingController: HttpTestingController;
    let employeeService: EmployeeService;
  
    beforeEach(() => {
      TestBed.configureTestingModule({
        imports: [ HttpClientTestingModule ],
        schemas: [NO_ERRORS_SCHEMA],
        providers: [ EmployeeService ]
      });
  
      httpClient = TestBed.get(HttpClient);
      httpTestingController = TestBed.get(HttpTestingController);
      employeeService = TestBed.get(EmployeeService);
    });

  afterEach(() => {
    httpTestingController.verify();
  });

  describe('#employee http services', () => {
    let expectedSmartEmployees: SmartEmployee[];
    let expectedEmployees: Employee[];

    beforeEach(() => {

        employeeService = TestBed.get(EmployeeService);

      expectedSmartEmployees = [
        { employeeID: 1, fullName: 'A' }, 
        { employeeID: 2, fullName: 'B' },
       ] as SmartEmployee[];

       expectedEmployees = [
        { employeeID: 1, fullName: 'A'}, 
        { employeeID: 2, fullName: 'A'}
      ] as Employee[];

    });


    it('should return expected employee (GetEmployeeByID)', () => {   
      employeeService.getEmployeeByID(1)
          .subscribe(employee => {
            expect(employee).toEqual(expectedEmployees[0]);
          });    
        const req = httpTestingController.expectOne('http://localhost:58653/api/employee/GetEmployee/'+1);    
        expect(req.request.method).toEqual('GET');    
        req.flush(expectedEmployees[0]);
      });


      
      it('should return expected employee (AddEmployee)', () => {
    
        employeeService.createEmployee(expectedEmployees[0])
          .subscribe(employee => {
            expect(employee).toEqual(expectedEmployees[0]);
          });
    
        const req = httpTestingController.expectOne('http://localhost:58653/api/employee/InsertEmployee');    
        expect(req.request.method).toEqual('POST');    
        req.flush(expectedEmployees[0]);
      });  


    it('should return expected Employees (GetEmployees)', () => {
    
        employeeService.getsmartEmployees()
          .subscribe(smartDetention => {
            expect(smartDetention).toEqual(expectedSmartEmployees);
          });    
        const req = httpTestingController.expectOne('http://localhost:58653/api/employee/GetEmployees/');    
        expect(req.request.method).toEqual('GET');    
        req.flush(expectedSmartEmployees);
      });


      it('should return expected employee (UpdateEmployee)', () => {

        employeeService.updateEmployee(expectedEmployees[0])
          .subscribe(enployee => {
            expect(enployee).toEqual(expectedEmployees[0]);
          });    
        const req = httpTestingController.expectOne('http://localhost:58653/api/employee/UpdateEmployee/'+expectedEmployees[0].employeeID);    
        expect(req.request.method).toEqual('PUT');   
        expect(req.request.body).toEqual(expectedEmployees[0]); 
        req.flush(expectedEmployees[0]);
      });


      it('should return expected employee (DeleteEmployee)', () => {

        employeeService.deleteEmployee(expectedEmployees[0].employeeID)
          .subscribe(employee => {
            expect(employee).toEqual(expectedEmployees[0]);
          });    
        const req = httpTestingController.expectOne('http://localhost:58653/api/employee/DeleteEmployee/'+expectedEmployees[0].employeeID);    
        expect(req.request.method).toEqual('DELETE');    
        req.flush(expectedEmployees[0]);
      });

      it('should return expected employees (SearchEmployeesByName)', () => {
 
        employeeService.searchEmployees(expectedSmartEmployees[0].fullName).subscribe(
          smartDetentions => {         
              expect(smartDetentions).toEqual(expectedSmartEmployees, 'should return expected heroes')},
          fail
        );    
  
        const req = httpTestingController.expectOne('http://localhost:58653/api/employee/GetEmploy?term='+expectedSmartEmployees[0].fullName);
        expect(req.request.method).toEqual('GET');
  
        req.flush(expectedSmartEmployees);
      }); 
    });
});

