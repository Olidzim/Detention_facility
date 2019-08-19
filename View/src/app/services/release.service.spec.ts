import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { NO_ERRORS_SCHEMA, DebugElement } from '@angular/core';

import { TestBed } from '@angular/core/testing';
import { HttpClient, HttpResponse, HttpErrorResponse } from '@angular/common/http';

import { asyncData, asyncError } from '../../testing/async-observable-helpers';

import { SmartRelease   } from '../models/smartrelease';
import { DatePipe } from '@angular/common';
import { ReleaseService } from './release.service';
import { Release } from '../models/release';

describe ('releaseService (with spies)', () => {
  let httpClientSpy: { get: jasmine.Spy };
  let httpClientSpyPost: { post: jasmine.Spy };
  let releaseService: ReleaseService;
  let datepipeSpy: {};

  beforeEach(() => {
   
    httpClientSpy = jasmine.createSpyObj('HttpClient', ['get'] );
    httpClientSpyPost = jasmine.createSpyObj('HttpClient', ['post'] );
    releaseService = new ReleaseService(<any> httpClientSpy, <any> datepipeSpy);
  });

  it('should return expected expectedSmartreleases (HttpClient called once)', () => {
    const expectedReleases: Release[] = [
        { releaseID: 1, releaseDate: new Date }, 
        { releaseID: 2, releaseDate: new Date }];


    httpClientSpy.get.and.returnValue(asyncData(expectedReleases));

    releaseService.getsmartReleases().subscribe(
      smartDetentions => expect(smartDetentions).toEqual(expectedReleases, 'expectedSmartreleases'),
      fail
    );
    expect(httpClientSpy.get.calls.count()).toBe(1, 'one call');
  });
});

describe('releaseService (with mocks)', () => {
    let httpClient: HttpClient;
    let httpTestingController: HttpTestingController;
    let releaseService: ReleaseService;
  
    beforeEach(() => {
      TestBed.configureTestingModule({
        imports: [ HttpClientTestingModule ],
        schemas: [NO_ERRORS_SCHEMA],
        providers: [ ReleaseService, DatePipe ]
      });
  
      httpClient = TestBed.get(HttpClient);
      httpTestingController = TestBed.get(HttpTestingController);
      releaseService = TestBed.get(ReleaseService);
    });

  afterEach(() => {
    httpTestingController.verify();
  });

  describe('#release http services', () => {
    let expectedSmartReleases: SmartRelease[];
    let expectedReleases: Release[];

    beforeEach(() => {

        releaseService = TestBed.get(ReleaseService);

      expectedSmartReleases = [
        { releaseID: 1, releaseDate: new Date }, 
        { releaseID: 2, releaseDate: new Date }
       ] as SmartRelease[];

       expectedReleases = [
        { releaseID: 1, releaseDate: new Date }, 
        { releaseID: 2, releaseDate: new Date }
      ] as Release[];

    });


    it('should return expected release (GetReleaseByIDs)', () => {
    
        
        releaseService.getReleaseByIDs(1,1)
          .subscribe(release => {
            expect(release).toEqual(expectedReleases[0]);
          });    
        const req = httpTestingController.expectOne('http://localhost:58653/api/release/GetReleaseByIDs/'+1+'/'+1);    
        expect(req.request.method).toEqual('GET');    
        req.flush(expectedReleases[0]);
      });


    it('should return expected release (GetReleaseByDate)', () => {
    
        
        releaseService.getSmartReleasesByDate(expectedReleases[0].releaseDate)
          .subscribe(release => {
            expect(release).toEqual(expectedReleases);
          });    
        const req = httpTestingController.expectOne('http://localhost:58653/api/release/GetSmartReleasesByDate/');    
        expect(req.request.method).toEqual('POST');    
        req.flush(expectedReleases);
      });


      it('should return expected release (GetreleaseByID)', () => {
    
        
        releaseService.getReleaseByID(1)
          .subscribe(release => {
            expect(release).toEqual(expectedReleases[0]);
          });    
        const req = httpTestingController.expectOne('http://localhost:58653/api/release/GetRelease/'+1);    
        expect(req.request.method).toEqual('GET');    
        req.flush(expectedReleases[0]);
      });

      
      it('should return expected release (Addrelease)', () => {
    
        releaseService.createRelease(expectedReleases[0])
          .subscribe(release => {
            expect(release).toEqual(expectedReleases[0]);
          });
    
        const req = httpTestingController.expectOne('http://localhost:58653/api/release/InsertRelease');    
        expect(req.request.method).toEqual('POST');    
        req.flush(expectedReleases[0]);
      });  


    it('should return expected smartDetentions (GetSmartReleases)', () => {
    
        releaseService.getsmartReleases()
          .subscribe(smartDetention => {
            expect(smartDetention).toEqual(expectedSmartReleases);
          });    
        const req = httpTestingController.expectOne('http://localhost:58653/api/release/GetReleases');    
        expect(req.request.method).toEqual('GET');    
        req.flush(expectedSmartReleases);
      });


      it('should return expected detention (UpdateRelease)', () => {

        releaseService.updateRelease(expectedReleases[0])
          .subscribe(enployee => {
            expect(enployee).toEqual(expectedReleases[0]);
          });    
        const req = httpTestingController.expectOne('http://localhost:58653/api/release/UpdateRelease/'+expectedReleases[0].releaseID);    
        expect(req.request.method).toEqual('PUT');   
        expect(req.request.body).toEqual(expectedReleases[0]); 
        req.flush(expectedReleases[0]);
      });


      it('should return expected release (DeleteRelease)', () => {

        releaseService.deleteRelease(expectedReleases[0].releaseID)
          .subscribe(release => {
            expect(release).toEqual(expectedReleases[0]);
          });    
        const req = httpTestingController.expectOne('http://localhost:58653/api/release/DeleteRelease/'+expectedReleases[0].releaseID);    
        expect(req.request.method).toEqual('DELETE');    
        req.flush(expectedReleases[0]);
      });
    
    });
});

