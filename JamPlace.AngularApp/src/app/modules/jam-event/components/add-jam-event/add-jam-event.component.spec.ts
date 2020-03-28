import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddJamEventComponent } from './add-jam-event.component';

describe('AddJamEventComponent', () => {
  let component: AddJamEventComponent;
  let fixture: ComponentFixture<AddJamEventComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddJamEventComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddJamEventComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
