import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FindDetaineeComponent } from './find-detainee.component';

describe('FindDetaineeComponent', () => {
  let component: FindDetaineeComponent;
  let fixture: ComponentFixture<FindDetaineeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FindDetaineeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FindDetaineeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
