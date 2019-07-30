import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddDetaineeComponent } from './add-detainee.component';

describe('AddDetaineeComponent', () => {
  let component: AddDetaineeComponent;
  let fixture: ComponentFixture<AddDetaineeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddDetaineeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddDetaineeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
