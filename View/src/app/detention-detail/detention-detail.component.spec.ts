import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DetentionDetailComponent } from './detention-detail.component';

describe('DetentionDetailComponent', () => {
  let component: DetentionDetailComponent;
  let fixture: ComponentFixture<DetentionDetailComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DetentionDetailComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DetentionDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
