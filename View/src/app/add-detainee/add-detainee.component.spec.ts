import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By }     from '@angular/platform-browser';
import { AddDetaineeComponent } from './add-detainee.component';
import { NO_ERRORS_SCHEMA, DebugElement } from '@angular/core';
import { HttpClientModule, HTTP_INTERCEPTORS} from "@angular/common/http";

import { FormsModule, ReactiveFormsModule } from '@angular/forms';
describe('AddDetaineeComponent', () => {
  let component: AddDetaineeComponent;
  let fixture: ComponentFixture<AddDetaineeComponent>;


  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [  AddDetaineeComponent ],
      schemas: [NO_ERRORS_SCHEMA],
      imports: [ReactiveFormsModule, FormsModule,HttpClientModule],
    })
    .compileComponents();
  }));

  
  beforeEach(() => {
    fixture = TestBed.createComponent(AddDetaineeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });


  it('expect sendDetaineeToDetention() toHaveBeenCalled', async(() => {      
    fixture.detectChanges();
    spyOn(component, 'sendDetaineeToDetention');
    const contextMenuEl: DebugElement[] =  fixture.debugElement.queryAll(By.css("button"));  
    contextMenuEl[0].triggerEventHandler("click" , null); 
    fixture.whenStable().then(() => {     
    expect(component. sendDetaineeToDetention).toHaveBeenCalled();
    });

  }));
});
