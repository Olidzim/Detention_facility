/*6*/
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { NO_ERRORS_SCHEMA, DebugElement } from '@angular/core';

import { TestBed } from '@angular/core/testing';
import { HttpClient, HttpResponse, HttpErrorResponse } from '@angular/common/http';

import { asyncData, asyncError } from '../../testing/async-observable-helpers';

import { DatePipe } from '@angular/common';
import { UserService } from './user.service';
import { User } from '../models/user';

describe ('userService (with spies)', () => {
  let httpClientSpy: { get: jasmine.Spy };
  let httpClientSpyPost: { post: jasmine.Spy };
  let userService: UserService;
  let datepipeSpy: {};

  beforeEach(() => {
   
    httpClientSpy = jasmine.createSpyObj('HttpClient', ['get'] );
    httpClientSpyPost = jasmine.createSpyObj('HttpClient', ['post'] );
    userService = new UserService(<any> httpClientSpy);
  });

  it('should return expected expectedSmartusers (HttpClient called once)', () => {
    const expectedUsers: User[] = [
        { userID: 1, login: 'A' }, 
        { userID: 2, login: 'B' }];


    httpClientSpy.get.and.returnValue(asyncData(expectedUsers));

    userService.getUsers().subscribe(
      smartDetentions => expect(smartDetentions).toEqual(expectedUsers, 'expectedUsers'),
      fail
    );
    expect(httpClientSpy.get.calls.count()).toBe(1, 'one call');
  });
});

describe('UserService (with mocks)', () => {
    let httpClient: HttpClient;
    let httpTestingController: HttpTestingController;
    let userService: UserService;
  
    beforeEach(() => {
      TestBed.configureTestingModule({
        imports: [ HttpClientTestingModule ],
        schemas: [NO_ERRORS_SCHEMA],
        providers: [ UserService, DatePipe ]
      });
  
      httpClient = TestBed.get(HttpClient);
      httpTestingController = TestBed.get(HttpTestingController);
      userService = TestBed.get(UserService);
    });

  afterEach(() => {
    httpTestingController.verify();
  });

  describe('#user http services', () => {

    let expectedUsers: User[];

    beforeEach(() => {

        userService = TestBed.get(UserService);     

       expectedUsers = [
        { userID: 1, login: 'A' }, 
        { userID: 2, login: 'B' }
      ] as User[];

    });


    it('should return expected user (GetuserByID)', () => {   
         userService.getUserByID(1)
          .subscribe(user => {
            expect(user).toEqual(expectedUsers[0]);
          });    
        const req = httpTestingController.expectOne('http://localhost:58653/api/account/GetUser/'+1);    
        expect(req.request.method).toEqual('GET');    
        req.flush(expectedUsers[0]);
      });

      
      it('should return expected user (Adduser)', () => {
    
        userService.createUser(expectedUsers[0])
          .subscribe(user => {
            expect(user).toEqual(expectedUsers[0]);
          });
    
        const req = httpTestingController.expectOne('http://localhost:58653/api/account/RegisterUser');    
        expect(req.request.method).toEqual('POST');    
        req.flush(expectedUsers[0]);
      });  


    it('should return expected users (GetUsers)', () => {
    
        userService.getUsers()
          .subscribe(smartDetention => {
            expect(smartDetention).toEqual(expectedUsers);
          });    
        const req = httpTestingController.expectOne('http://localhost:58653/api/account/GetUsers/');    
        expect(req.request.method).toEqual('GET');    
        req.flush(expectedUsers);
      });


      it('should return expected user (UpdateUser)', () => {

        userService.updateUser(expectedUsers[0])
          .subscribe(enployee => {
            expect(enployee).toEqual(expectedUsers[0]);
          });    
        const req = httpTestingController.expectOne('http://localhost:58653/api/account/UpdateUser/'+expectedUsers[0].userID);    
        expect(req.request.method).toEqual('PUT');   
        expect(req.request.body).toEqual(expectedUsers[0]); 
        req.flush(expectedUsers[0]);
      });


      it('should return expected user (DeleteUser)', () => {

        userService.deleteUser(expectedUsers[0].userID)
          .subscribe(user => {
            expect(user).toEqual(expectedUsers[0]);
          });    
        const req = httpTestingController.expectOne('http://localhost:58653/api/account/DeleteUser/'+expectedUsers[0].userID);    
        expect(req.request.method).toEqual('DELETE');    
        req.flush(expectedUsers[0]);
      });
    
    });
});

