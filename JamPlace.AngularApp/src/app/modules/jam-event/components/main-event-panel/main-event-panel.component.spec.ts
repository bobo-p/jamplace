import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MainEventPanelComponent } from './main-event-panel.component';

describe('MainEventPanelComponent', () => {
  let component: MainEventPanelComponent;
  let fixture: ComponentFixture<MainEventPanelComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MainEventPanelComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MainEventPanelComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
