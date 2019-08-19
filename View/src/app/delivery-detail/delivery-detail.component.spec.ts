import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By }     from '@angular/platform-browser';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientModule, HTTP_INTERCEPTORS} from "@angular/common/http";
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NO_ERRORS_SCHEMA, DebugElement } from '@angular/core';
import { Router } from '@angular/router';

import { DatePipe } from '@angular/common';

import { PermissionManagerService } from '../role/permission-manager.service';
import { IsGrantedDirective } from '../role/is-granted.directive';

import { DeliveryDetailComponent } from './delivery-detail.component';

describe('DeliveryComponent', () => {
  let component: DeliveryDetailComponent;
  let fixture: ComponentFixture<DeliveryDetailComponent>;
  let mockRouter = {
    navigate: jasmine.createSpy('/home/detainee/')
  } 

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DeliveryDetailComponent, IsGrantedDirective  ],
      schemas: [NO_ERRORS_SCHEMA],
      providers: [
        PermissionManagerService, DatePipe, {provide:Router, useValue: mockRouter}
       ],
       imports: [
        HttpClientModule
      ],
    })
    .compileComponents();
  }));
  
  beforeEach(() => {
    fixture = TestBed.createComponent(DeliveryDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

});
