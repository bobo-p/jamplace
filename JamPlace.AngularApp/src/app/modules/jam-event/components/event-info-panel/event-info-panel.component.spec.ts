import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EventInfoPanelComponent } from './event-info-panel.component';

describe('EventInfoPanelComponent', () => {
  let component: EventInfoPanelComponent;
  let fixture: ComponentFixture<EventInfoPanelComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EventInfoPanelComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EventInfoPanelComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
