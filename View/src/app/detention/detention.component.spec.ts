import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { NO_ERRORS_SCHEMA, DebugElement } from '@angular/core';
import { asyncData } from '../../testing/async-observable-helpers';
import { DetentionComponent } from './detention.component';
import { By }     from '@angular/platform-browser';
import { Router } from '@angular/router';
import { DetentionService } from '../services/detention.service';
import { getTestDetentions } from '../models/test-detentions';
import { IsGrantedDirective } from '../role/is-granted.directive';
import { HttpClientModule, HTTP_INTERCEPTORS} from "@angular/common/http";
import { PermissionManagerService } from '../role/permission-manager.service';
import { Detention } from '../models/detention';
import { SmartDelivery } from '../models/smartdelivery';
import { SmartDetention } from '../models/smartdetention';

let comp: DetentionComponent;
let fixture: ComponentFixture<DetentionComponent>;

describe('DetentionComponent (shallow)', () => {
  beforeEach(() => {
    console.log("111")
    TestBed.configureTestingModule({
      declarations: [ DetentionComponent, IsGrantedDirective ],
      imports: [
        HttpClientModule,
      ],
      schemas: [NO_ERRORS_SCHEMA],
      providers: [
        PermissionManagerService,
       ]
    });
  });

compileAndCreate();  
tests();
 afterEach(() => {
  TestBed.resetTestingModule();
});
});

function compileAndCreate() {
  beforeEach(async(() => {
    const routerSpy = jasmine.createSpyObj('Router', ['navigateByUrl']);
    const detentionServiceSpy = jasmine.createSpyObj('DetentionService', ['getSmartDetentions']);
  

    TestBed.configureTestingModule({
      providers: [        
        {
          provide: Router, useValue: routerSpy 
        },
        {
          provide : DetentionService, useValue: detentionServiceSpy,
        }
      ]
    })
    .compileComponents().then(() => {
      fixture = TestBed.createComponent(DetentionComponent);
      comp = fixture.componentInstance;         
   
      detentionServiceSpy.getSmartDetentions.and.returnValue(asyncData(getTestDetentions()))
      comp.loadsmartDetentions();
    
    });


  }));
  afterEach(() => {
    TestBed.resetTestingModule();
    fixture.destroy();
  });
}


function tests() {

  describe('expect toDetentionDetail toHaveBeenCalled', () => { 

    it('should test selected', async(() => {      
      fixture.detectChanges();
      spyOn(comp, 'toDetentionDetail');
      const contextMenuEl: DebugElement[] =  fixture.debugElement.queryAll(By.css("tr"));
      let d = comp.detentions[0]
      contextMenuEl[1].triggerEventHandler("dblclick" , d); 
      fixture.whenStable().then(() => {     
      expect(comp.toDetentionDetail).toHaveBeenCalled();
    });
  }));

  it('expect ROUTER to navigate when toDetentionDetail called', async(() => {
    let router : Router
    router = fixture.debugElement.injector.get(Router);

    comp.toDetentionDetail(comp.detentions[0])
    const spy = router.navigateByUrl as jasmine.Spy;
    const navArgs = spy.calls.first().args[0];
    console.log("nav"+navArgs)
    const id = comp.detentions[0].detentionID;
    expect(navArgs).toBe('/home/detention/detention-detail/' + id,
    'should nav to HeroDetail for first hero');
    }));  
  });

}