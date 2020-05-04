import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CustomSearchFieldComponent } from './custom-search-field.component';

describe('CustomSearchFieldComponent', () => {
  let component: CustomSearchFieldComponent;
  let fixture: ComponentFixture<CustomSearchFieldComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CustomSearchFieldComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CustomSearchFieldComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
