import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddDetentionComponent } from './add-detention.component';

describe('DetentionComponent', () => {
  let component: AddDetentionComponent;
  let fixture: ComponentFixture<AddDetentionComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddDetentionComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddDetentionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
