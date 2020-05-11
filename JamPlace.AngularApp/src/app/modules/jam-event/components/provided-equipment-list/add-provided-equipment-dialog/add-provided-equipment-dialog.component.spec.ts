import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddProvidedEquipmentDialogComponent } from './add-provided-equipment-dialog.component';

describe('AddProvidedEquipmentDialogComponent', () => {
  let component: AddProvidedEquipmentDialogComponent;
  let fixture: ComponentFixture<AddProvidedEquipmentDialogComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddProvidedEquipmentDialogComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddProvidedEquipmentDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
