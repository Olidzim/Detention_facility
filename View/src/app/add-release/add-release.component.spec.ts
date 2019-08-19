import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { NO_ERRORS_SCHEMA, DebugElement } from '@angular/core';
import { AddReleaseComponent } from './add-release.component';
import { HttpClientModule, HTTP_INTERCEPTORS} from "@angular/common/http";
import { DatePipe } from '@angular/common';
import { ReleaseService } from '../services/release.service';
import { Release } from '../models/release';
import { By }     from '@angular/platform-browser';

describe('AddReleaseComponent', () => {
  let component: AddReleaseComponent;
  let fixture: ComponentFixture<AddReleaseComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddReleaseComponent ],
      schemas: [NO_ERRORS_SCHEMA],
      imports: [HttpClientModule],
      providers: [ ReleaseService, DatePipe ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddReleaseComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });


  it('should create', () => {
    expect(component).toBeTruthy();
  });


  it('expect save() to have been called', async(() => {      
    fixture.detectChanges();
    spyOn(component, 'save');
    const contextMenuEl: DebugElement[] =  fixture.debugElement.queryAll(By.css("button"));  
    contextMenuEl[0].triggerEventHandler("click" , null); 
    fixture.whenStable().then(() => {     
    expect(component.save).toHaveBeenCalled();
    });
  }));


  it('expect cancel() to have been called', async(() => {      
    fixture.detectChanges();
    spyOn(component, 'cancel');
    const contextMenuEl: DebugElement[] =  fixture.debugElement.queryAll(By.css("button"));  
    contextMenuEl[1].triggerEventHandler("click" , null); 
    fixture.whenStable().then(() => {     
    expect(component.cancel).toHaveBeenCalled();
    });

  }));
});
