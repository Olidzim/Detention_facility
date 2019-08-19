import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { NO_ERRORS_SCHEMA, DebugElement } from '@angular/core';
import { AddDetentionComponent } from './add-detention.component';
import { By }     from '@angular/platform-browser';
import { HttpClientModule, HTTP_INTERCEPTORS} from "@angular/common/http";
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { DatePipe } from '@angular/common';
import { Detainee } from '../models/detainee';

describe('DetentionComponent', () => {
  let component: AddDetentionComponent;
  let fixture: ComponentFixture<AddDetentionComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddDetentionComponent ],
      schemas: [NO_ERRORS_SCHEMA],
      imports: [ReactiveFormsModule, FormsModule, HttpClientModule],
      providers: [  DatePipe ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddDetentionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });


  it('expect addDetention toHaveBeenCalled', async(() => {    
   component.detainees = [
      { detaineeID: 1, firstName: 'A', residencePlace: 'somePlace'}, 
      { detaineeID: 2, firstname: 'A', residencePlace: 'somePlace'}
    ] as Detainee[];
    
    fixture.detectChanges();
    console.log(component.detainees[0])

    spyOn(component, 'addDetention');
    const contextMenuEl: DebugElement[] =  fixture.debugElement.queryAll(By.css("button"));  
    contextMenuEl[2].triggerEventHandler("click" , null); 
    fixture.whenStable().then(() => {     
    expect(component.addDetention).toHaveBeenCalled();
    });
  }));

});
