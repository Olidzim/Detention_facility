import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DetaineeDetailComponent } from './detainee-detail.component';

describe('DetaineeDetailComponent', () => {
  let component: DetaineeDetailComponent;
  let fixture: ComponentFixture<DetaineeDetailComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DetaineeDetailComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DetaineeDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
