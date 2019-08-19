import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { NO_ERRORS_SCHEMA, DebugElement } from '@angular/core';

import { TestBed } from '@angular/core/testing';
import { HttpClient, HttpResponse, HttpErrorResponse } from '@angular/common/http';

import { asyncData, asyncError } from '../../testing/async-observable-helpers';

import { SmartDelivery   } from '../models/smartdelivery';
import { DatePipe } from '@angular/common';
import { DeliveryService } from './delivery.service';
import { Delivery } from '../models/delivery';

describe ('deliveryService (with spies)', () => {
  let httpClientSpy: { get: jasmine.Spy };
  let httpClientSpyPost: { post: jasmine.Spy };
  let deliveryService: DeliveryService;
  let datepipeSpy: {};

  beforeEach(() => {
   
    httpClientSpy = jasmine.createSpyObj('HttpClient', ['get'] );
    httpClientSpyPost = jasmine.createSpyObj('HttpClient', ['post'] );
    deliveryService = new DeliveryService(<any> httpClientSpy, <any> datepipeSpy);
  });

  it('should return expected expectedSmartdeliverys (HttpClient called once)', () => {
    const expectedDeliverys: Delivery[] = [
        { deliveryID: 1, deliveryDate: new Date }, 
        { deliveryID: 2, deliveryDate: new Date }];


    httpClientSpy.get.and.returnValue(asyncData(expectedDeliverys));

    deliveryService.getsmartDeliverys().subscribe(
      smartDetentions => expect(smartDetentions).toEqual(expectedDeliverys, 'expectedSmartdeliverys'),
      fail
    );
    expect(httpClientSpy.get.calls.count()).toBe(1, 'one call');
  });
});

describe('DeliveryService (with mocks)', () => {
    let httpClient: HttpClient;
    let httpTestingController: HttpTestingController;
    let deliveryService: DeliveryService;
  
    beforeEach(() => {
      TestBed.configureTestingModule({
        imports: [ HttpClientTestingModule ],
        schemas: [NO_ERRORS_SCHEMA],
        providers: [ DeliveryService, DatePipe ]
      });
  
      httpClient = TestBed.get(HttpClient);
      httpTestingController = TestBed.get(HttpTestingController);
      deliveryService = TestBed.get(DeliveryService);
    });

  afterEach(() => {
    httpTestingController.verify();
  });

  describe('#delivery http services', () => {
    let expectedSmartDeliverys: SmartDelivery[];
    let expectedDeliverys: Delivery[];

    beforeEach(() => {

        deliveryService = TestBed.get(DeliveryService);

      expectedSmartDeliverys = [
        { deliveryID: 1, deliveryDate: new Date }, 
        { deliveryID: 2, deliveryDate: new Date }
       ] as SmartDelivery[];

       expectedDeliverys = [
        { deliveryID: 1, deliveryDate: new Date }, 
        { deliveryID: 2, deliveryDate: new Date }
      ] as Delivery[];

    });


    it('should return expected delivery (GetDeliveryByIDs)', () => {
    
        
        deliveryService.getsmartDeliverysByIDs(1,1)
          .subscribe(delivery => {
            expect(delivery).toEqual(expectedDeliverys[0]);
          });    
        const req = httpTestingController.expectOne('http://localhost:58653/api/delivery/GetDeliveryByIDs/'+1+'/'+1);    
        expect(req.request.method).toEqual('GET');    
        req.flush(expectedDeliverys[0]);
      });


      it('should return expected delivery (GetDeliveryByIDs)', () => {
    
        
        deliveryService.getDelivery(1,1)
          .subscribe(delivery => {
            expect(delivery).toEqual(expectedDeliverys[0]);
          });    
        const req = httpTestingController.expectOne('http://localhost:58653/api/delivery/GetDeliveryByIDs/'+1+'/'+1);    
        expect(req.request.method).toEqual('GET');    
        req.flush(expectedDeliverys[0]);
      });


    it('should return expected delivery (GetdeliveryByDate)', () => {
    
        
        deliveryService.getSmartDeliveriesByDate(expectedDeliverys[0].deliveryDate)
          .subscribe(delivery => {
            expect(delivery).toEqual(expectedDeliverys);
          });    
        const req = httpTestingController.expectOne('http://localhost:58653/api/delivery/GetSmartDeliveriesByDate/');    
        expect(req.request.method).toEqual('POST');    
        req.flush(expectedDeliverys);
      });




      it('should return expected delivery (GetdeliveryByID)', () => {
    
        
        deliveryService.getDeliveryByID(1)
          .subscribe(delivery => {
            expect(delivery).toEqual(expectedDeliverys[0]);
          });    
        const req = httpTestingController.expectOne('http://localhost:58653/api/delivery/GetDelivery/'+1);    
        expect(req.request.method).toEqual('GET');    
        req.flush(expectedDeliverys[0]);
      });

      
      it('should return expected delivery (Adddelivery)', () => {
    
        deliveryService.createDelivery(expectedDeliverys[0])
          .subscribe(delivery => {
            expect(delivery).toEqual(expectedDeliverys[0]);
          });
    
        const req = httpTestingController.expectOne('http://localhost:58653/api/delivery/InsertDelivery');    
        expect(req.request.method).toEqual('POST');    
        req.flush(expectedDeliverys[0]);
      });  


    it('should return expected deliveries (GetSmartDeliveries)', () => {
    
        deliveryService.getsmartDeliverys()
          .subscribe(smartDetention => {
            expect(smartDetention).toEqual(expectedSmartDeliverys);
          });    
        const req = httpTestingController.expectOne('http://localhost:58653/api/delivery/GetDeliveries');    
        expect(req.request.method).toEqual('GET');    
        req.flush(expectedSmartDeliverys);
      });


      it('should return expected delivery (UpdateDelivery)', () => {

        deliveryService.updateDelivery(expectedDeliverys[0])
          .subscribe(enployee => {
            expect(enployee).toEqual(expectedDeliverys[0]);
          });    
        const req = httpTestingController.expectOne('http://localhost:58653/api/delivery/UpdateDelivery/'+expectedDeliverys[0].deliveryID);    
        expect(req.request.method).toEqual('PUT');   
        expect(req.request.body).toEqual(expectedDeliverys[0]); 
        req.flush(expectedDeliverys[0]);
      });


      it('should return expected delivery (DeleteDelivery)', () => {

        deliveryService.deleteDelivery(expectedDeliverys[0].deliveryID)
          .subscribe(delivery => {
            expect(delivery).toEqual(expectedDeliverys[0]);
          });    
        const req = httpTestingController.expectOne('http://localhost:58653/api/delivery/DeleteDelivery/'+expectedDeliverys[0].deliveryID);    
        expect(req.request.method).toEqual('DELETE');    
        req.flush(expectedDeliverys[0]);
      });
    
    });
});

