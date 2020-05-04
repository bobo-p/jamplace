import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddSongDialogComponent } from './add-song-dialog.component';

describe('AddSongDialogComponent', () => {
  let component: AddSongDialogComponent;
  let fixture: ComponentFixture<AddSongDialogComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddSongDialogComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddSongDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
