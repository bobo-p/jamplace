import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EditJamEventDialogComponent } from './edit-jam-event-dialog.component';

describe('EditJamEventDialogComponent', () => {
  let component: EditJamEventDialogComponent;
  let fixture: ComponentFixture<EditJamEventDialogComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EditJamEventDialogComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EditJamEventDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
