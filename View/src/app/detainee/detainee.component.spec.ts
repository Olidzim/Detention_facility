import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DetaineeComponent } from './detainee.component';

describe('DetaineeComponent', () => {
  let component: DetaineeComponent;
  let fixture: ComponentFixture<DetaineeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DetaineeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DetaineeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
