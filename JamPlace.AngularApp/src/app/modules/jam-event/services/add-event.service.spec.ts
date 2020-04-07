import { TestBed } from '@angular/core/testing';

import { AddEventService } from './add-event.service';

describe('AddEventService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: AddEventService = TestBed.get(AddEventService);
    expect(service).toBeTruthy();
  });
});
