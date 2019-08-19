import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { NO_ERRORS_SCHEMA, DebugElement } from '@angular/core';

import { TestBed } from '@angular/core/testing';
import { HttpClient, HttpResponse, HttpErrorResponse } from '@angular/common/http';

import { asyncData, asyncError } from '../../testing/async-observable-helpers';
import { DatePipe } from '@angular/common';
import { SmartDetention } from '../models/smartdetention';
import { Detention } from '../models/detention';
import { DetentionService } from './detention.service';

describe ('DetentionService (with spies)', () => {
  let httpClientSpy: { get: jasmine.Spy };
  let httpClientSpyPost: { post: jasmine.Spy };
  let datepipeSpy: {};
  let detentionService: DetentionService;

  beforeEach(() => {
   
    httpClientSpy = jasmine.createSpyObj('HttpClient', ['get'] );
    httpClientSpyPost = jasmine.createSpyObj('HttpClient', ['post'] );
    datepipeSpy = jasmine.createSpy('DatePipe');
    detentionService = new DetentionService(<any> httpClientSpy, <any> datepipeSpy);
  });

  it('should return expected expectedSmartDetentions (HttpClient called once)', () => {
    const expectedSmartDetentions: SmartDetention[] = [
        { detentionID: 1, detentionDate: new Date, employeeFullName: 'A' }, 
        { detentionID: 2, detentionDate: new Date , employeeFullName: 'B' }];



    httpClientSpy.get.and.returnValue(asyncData(expectedSmartDetentions));

    detentionService.getSmartDetentions().subscribe(
      smartDetentions => expect(smartDetentions).toEqual(expectedSmartDetentions, 'expectedSmartDetentions'),
      fail
    );
    expect(httpClientSpy.get.calls.count()).toBe(1, 'one call');
  });
});

describe('DetentionService (with mocks)', () => {
  let httpClient: HttpClient;
  let httpTestingController: HttpTestingController;
  let detentionService: DetentionService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [ HttpClientTestingModule ],
      schemas: [NO_ERRORS_SCHEMA],
      providers: [ DetentionService, DatePipe ]
    });

    httpClient = TestBed.get(HttpClient);
    httpTestingController = TestBed.get(HttpTestingController);
    detentionService = TestBed.get(DetentionService);
  });

  afterEach(() => {
    httpTestingController.verify();
  });

  describe('#Detention http services', () => {
    let expectedSmartDetentions: SmartDetention[];
    let expectedDetentions: Detention[];

    beforeEach(() => {
      detentionService = TestBed.get(DetentionService);

      expectedSmartDetentions = [
        { detentionID: 1, detentionDate: new Date, employeeFullName: 'A' }, 
        { detentionID: 2, detentionDate: new Date , employeeFullName: 'B' },
       ] as SmartDetention[];

       expectedDetentions = [
        { detentionID: 1, detentionDate: new Date, detainedByEmployeeID: 1 }, 
        { detentionID: 2, detentionDate: new Date , detainedByEmployeeID: 1 }
      ] as Detention[];     
    });


    it('should return expected smartDetentions (GetSmartDetentions)', () => {

      detentionService.getSmartDetentions().subscribe(
        smartDetentions => { 
            expect(smartDetentions).toEqual(expectedSmartDetentions, 'should return expected smartDetentions')},
        fail
      );

      const req = httpTestingController.expectOne('http://localhost:58653/api/detention/GetSmartDetentions');
      expect(req.request.method).toEqual('GET');

      req.flush(expectedSmartDetentions);
    });


    it('should return expected detentions (GetDetentionsByDate)', () => {
  
      detentionService.searchDetentionsByDate(expectedDetentions[0].detentionDate).subscribe(
        detentions => {      
            expect(detentions).toEqual(expectedSmartDetentions, 'should return expected heroes')},
        fail
      );     
 
      const req = httpTestingController.expectOne('http://localhost:58653/api/detention/GetDetentionsByDate/');
      expect(req.request.method).toEqual('POST');

      req.flush(expectedSmartDetentions);
    });


    it('should return expected smartDetention (GetSmartDetentionsByDetaineeID)', () => {
 
      detentionService.getSmartDetentionsByDetaineeID(1).subscribe(
        smartDetentions => {         
            expect(smartDetentions).toEqual(expectedSmartDetentions, 'should return expected heroes')},
        fail
      );    

      const req = httpTestingController.expectOne('http://localhost:58653/api/detention/GetSmartDetentionsByDetaineeID/'+1);
      expect(req.request.method).toEqual('GET');

      req.flush(expectedSmartDetentions);
    });


    it('should return expected smartDetention (GetSmartDetentionsByDetentionID)', () => {
  
      detentionService.getSmartDetentionByDetentionID(1).subscribe(
        smartDetention => {
            expect(smartDetention).toEqual(expectedSmartDetentions[0], 'should return expected heroes')},
        fail
      );    

      const req = httpTestingController.expectOne('http://localhost:58653/api/detention/GetSmartDetentionsByDetentionID/'+1);
      expect(req.request.method).toEqual('GET');

      req.flush(expectedSmartDetentions[0]);
    });


    it('should return expected detention (InsertDetention)', () => {
    
        detentionService.addDetention(expectedDetentions[0])
          .subscribe(detention => {
            expect(detention.detainedByEmployeeID).toEqual(1);
          });
    
        const req = httpTestingController.expectOne('http://localhost:58653/api/detention/InsertDetention/');    
        expect(req.request.method).toEqual('POST');    
        req.flush(expectedDetentions[0]);
      });


      it('should return expected detention (UpdateDetention)', () => {

        detentionService.updateDetention(expectedDetentions[0])
          .subscribe(detention => {
            expect(detention).toEqual(expectedDetentions[0]);
          });    
        const req = httpTestingController.expectOne('http://localhost:58653/api/detention/UpdateDetention/'+expectedDetentions[0].detentionID);    
        expect(req.request.method).toEqual('PUT');   
        expect(req.request.body).toEqual(expectedDetentions[0]); 
        req.flush(expectedDetentions[0]);
      });


      it('should return expected detention (DeleteDetention)', () => {

        detentionService.deleteDetention(expectedDetentions[0].detentionID)
          .subscribe(detention => {
            expect(detention).toEqual(expectedDetentions[0]);
          });    
        const req = httpTestingController.expectOne('http://localhost:58653/api/detention/DeleteDetention/'+expectedDetentions[0].detentionID);    
        expect(req.request.method).toEqual('DELETE');    
        req.flush(expectedDetentions[0]);
      });


      it('should return expected detention (GetDetentionByID)', () => {
    
        detentionService.getDetentionByDetentionID(1)
          .subscribe(detention => {
            expect(detention).toEqual(expectedDetentions[0]);
          });    
        const req = httpTestingController.expectOne('http://localhost:58653/api/detention/GetDetention/'+1);    
        expect(req.request.method).toEqual('GET');    
        req.flush(expectedDetentions[0]);
      });


      it('should return expected smartDetentions (GetSmartDetentions)', () => {
    
        detentionService.getSmartDetentions()
          .subscribe(smartDetention => {
            expect(smartDetention).toEqual(expectedSmartDetentions);
          });    
        const req = httpTestingController.expectOne('http://localhost:58653/api/detention/GetSmartDetentions');    
        expect(req.request.method).toEqual('GET');    
        req.flush(expectedSmartDetentions);
      });
  });
});

