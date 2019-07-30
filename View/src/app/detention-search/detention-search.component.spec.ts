import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DetentionSearchComponent } from './detention-search.component';

describe('DetentionSearchComponent', () => {
  let component: DetentionSearchComponent;
  let fixture: ComponentFixture<DetentionSearchComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DetentionSearchComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DetentionSearchComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
