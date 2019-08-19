import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { NO_ERRORS_SCHEMA, DebugElement } from '@angular/core';

import { TestBed } from '@angular/core/testing';
import { HttpClient, HttpResponse, HttpErrorResponse } from '@angular/common/http';

import { asyncData, asyncError } from '../../testing/async-observable-helpers';

import { SmartDetainee  } from '../models/smartdetainee';
import { Detainee } from '../models/detainee';
import { DetaineeService } from './detainee.service';
import { Detention } from '../models/detention';

describe ('detaineeService (with spies)', () => {
  let httpClientSpy: { get: jasmine.Spy };
  let httpClientSpyPost: { post: jasmine.Spy };
  let detaineeService: DetaineeService;

  beforeEach(() => {
   
    httpClientSpy = jasmine.createSpyObj('HttpClient', ['get'] );
    httpClientSpyPost = jasmine.createSpyObj('HttpClient', ['post'] );
    detaineeService = new DetaineeService(<any> httpClientSpy);
  });

  it('should return expected expectedSmartDetainees (HttpClient called once)', () => {
    const expectedSmartDetainees: SmartDetainee[] = [
        { detaineeID: 1, fullname: 'A' }, 
        { detaineeID: 2, fullname: 'B' }];


    httpClientSpy.get.and.returnValue(asyncData(expectedSmartDetainees));

    detaineeService.getsmartDetainees().subscribe(
      smartDetentions => expect(smartDetentions).toEqual(expectedSmartDetainees, 'expectedSmartDetainees'),
      fail
    );
    expect(httpClientSpy.get.calls.count()).toBe(1, 'one call');
  });
});

describe('DetentionService (with mocks)', () => {
    let httpClient: HttpClient;
    let httpTestingController: HttpTestingController;
    let detaineeService: DetaineeService;
  
    beforeEach(() => {
      TestBed.configureTestingModule({
        imports: [ HttpClientTestingModule ],
        schemas: [NO_ERRORS_SCHEMA],
        providers: [ DetaineeService ]
      });
  
      httpClient = TestBed.get(HttpClient);
      httpTestingController = TestBed.get(HttpTestingController);
      detaineeService = TestBed.get(DetaineeService);
    });

  afterEach(() => {
    httpTestingController.verify();
  });

  describe('#Detainee http services', () => {
    let expectedSmartDetainees: SmartDetainee[];
    let expectedDetainees: Detainee[];
    let expectedDetentions: Detention[];

    beforeEach(() => {

        detaineeService = TestBed.get(DetaineeService);

      expectedSmartDetainees = [
        { detaineeID: 1, fullname: 'A'}, 
        { detaineeID: 2, fullname: 'A' },
       ] as SmartDetainee[];

       expectedDetainees = [
        { detaineeID: 1, firstName: 'A', residencePlace: 'somePlace'}, 
        { detaineeID: 2, firstname: 'A', residencePlace: 'somePlace'}
      ] as Detainee[];

      expectedDetentions = [
        { detentionID: 1, detentionDate: new Date, detainedByEmployeeID: 1 }, 
        { detentionID: 2, detentionDate: new Date , detainedByEmployeeID: 1 }
      ] as Detention[];

    });


    it('should return expected detainee (GetDetaineeByID)', () => {
    
        
        detaineeService.getDetainee(1)
          .subscribe(detainee => {
            expect(detainee).toEqual(expectedDetainees[0]);
          });    
        const req = httpTestingController.expectOne('http://localhost:58653/api/detainee/GetDetaineeByID/'+1);    
        expect(req.request.method).toEqual('GET');    
        req.flush(expectedDetainees[0]);
      });


    it('should return expected smartDetainees (GetSmartDetaineesByDetentionID)', () => {

      detaineeService.getsmartDetaineesByDetentionID(expectedDetentions[0].detentionID).subscribe(
        smartDetainees => { 
            expect(smartDetainees).toEqual(expectedSmartDetainees, 'should return expected smartDetainees')},
        fail
      );

      const req = httpTestingController.expectOne('http://localhost:58653/api/detainee/GetDetaineeByDetentionID/'+expectedDetentions[0].detentionID);
      expect(req.request.method).toEqual('GET');

      req.flush(expectedSmartDetainees);
    });


    it('should return expected smartDetentions (GetSmartDetentions)', () => {
    
        detaineeService.getsmartDetainees()
          .subscribe(smartDetention => {
            expect(smartDetention).toEqual(expectedSmartDetainees);
          });    
        const req = httpTestingController.expectOne('http://localhost:58653/api/detainee/GetDetainees/');    
        expect(req.request.method).toEqual('GET');    
        req.flush(expectedSmartDetainees);
      });

      it('should return expected Detainees (SearchDetaineesByName)', () => {
 
        detaineeService.searchDetaineesByName(expectedDetainees[0].firstName).subscribe(
          smartDetentions => {         
              expect(smartDetentions).toEqual(expectedDetainees, 'should return expected heroes')},
          fail
        );    
  
        const req = httpTestingController.expectOne('http://localhost:58653/api/detainee/GetDet?term='+expectedDetainees[0].firstName);
        expect(req.request.method).toEqual('GET');
  
        req.flush(expectedDetainees);
      });   

      it('should return expected Detainees (SearchDetaineesByAddress)', () => {
 
        detaineeService.searchDetaineesByAddress(expectedDetainees[0].residencePlace).subscribe(
          smartDetentions => {         
              expect(smartDetentions).toEqual(expectedDetainees, 'should return expected heroes')},
          fail
        );    
  
        const req = httpTestingController.expectOne('http://localhost:58653/api/detainee/GetDetaineeByAddress?term='+expectedDetainees[0].residencePlace);
        expect(req.request.method).toEqual('GET');
  
        req.flush(expectedDetainees);
      });  
      
      
      it('should return expected detainee (UpdateDetainee)', () => {

        detaineeService.updateDetainee(expectedDetainees[0])
          .subscribe(detention => {
            expect(detention).toEqual(expectedDetainees[0]);
          });    
        const req = httpTestingController.expectOne('http://localhost:58653/api/detainee/UpdateDetainee/'+expectedDetainees[0].detaineeID);    
        expect(req.request.method).toEqual('PUT');   
        expect(req.request.body).toEqual(expectedDetainees[0]); 
        req.flush(expectedDetainees[0]);
      });


      it('should return expected detention (AddDetaineeToDetention)', () => {
    
        detaineeService.addDetaineeToDetention(expectedDetentions[0].detentionID, expectedDetainees[0].detaineeID)
          .subscribe(detention => {
            expect(detention).toEqual(expectedDetentions[0]);
          });
    
        const req = httpTestingController.expectOne('http://localhost:58653/api/detainee/AddDetaineeToDetention/'+expectedDetainees[0].detaineeID);    
        expect(req.request.method).toEqual('POST');    
        req.flush(expectedDetentions[0]);
      });


      it('should return expected detainee (AddDetainee)', () => {
    
        detaineeService.addDetainee(expectedDetainees[0])
          .subscribe(detainee=> {
            expect(detainee).toEqual(expectedDetainees[0]);
          });
    
        const req = httpTestingController.expectOne('http://localhost:58653/api/detainee/InsertDetainee/');    
        expect(req.request.method).toEqual('POST');    
        req.flush(expectedDetainees[0]);
      });


      it('should return expected detainee (DeleteDetainee)', () => {

        detaineeService.deleteDetainee(expectedDetainees[0].detaineeID)
          .subscribe(detainee => {
            expect(detainee).toEqual(expectedDetainees[0]);
          });    
        const req = httpTestingController.expectOne('http://localhost:58653/api/detainee/DeleteDetainee/'+expectedDetainees[0].detaineeID);    
        expect(req.request.method).toEqual('DELETE');    
        req.flush(expectedDetainees[0]);
      });
  });
});

