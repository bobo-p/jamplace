import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ProvidedEquipmentListComponent } from './provided-equipment-list.component';

describe('ProvidedEquipmentListComponent', () => {
  let component: ProvidedEquipmentListComponent;
  let fixture: ComponentFixture<ProvidedEquipmentListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ProvidedEquipmentListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProvidedEquipmentListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
