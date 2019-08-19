import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { NO_ERRORS_SCHEMA, DebugElement } from '@angular/core';
import { AddDeliveryComponent } from './add-delivery.component';
import { HttpClientModule, HTTP_INTERCEPTORS} from "@angular/common/http";
import { DatePipe } from '@angular/common';
import { DeliveryService } from '../services/delivery.service';
import { Delivery } from '../models/delivery';
import { By }     from '@angular/platform-browser';

describe('AddDeliveryComponent', () => {
  let component: AddDeliveryComponent;
  let fixture: ComponentFixture<AddDeliveryComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddDeliveryComponent ],
      schemas: [NO_ERRORS_SCHEMA],
      imports: [HttpClientModule],
      providers: [ DeliveryService, DatePipe ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddDeliveryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });


  it('should create', () => {
    expect(component).toBeTruthy();
  });


  it('expect save() toHaveBeenCalled', async(() => {      
    fixture.detectChanges();
    spyOn(component, 'save');
    const contextMenuEl: DebugElement[] =  fixture.debugElement.queryAll(By.css("button"));  
    contextMenuEl[0].triggerEventHandler("click" , null); 
    fixture.whenStable().then(() => {     
    expect(component.save).toHaveBeenCalled();
    });
  }));


  it('expect cancel() toHaveBeenCalled', async(() => {      
    fixture.detectChanges();
    spyOn(component, 'cancel');
    const contextMenuEl: DebugElement[] =  fixture.debugElement.queryAll(By.css("button"));  
    contextMenuEl[1].triggerEventHandler("click" , null); 
    fixture.whenStable().then(() => {     
    expect(component.cancel).toHaveBeenCalled();
    });

  }));
});
