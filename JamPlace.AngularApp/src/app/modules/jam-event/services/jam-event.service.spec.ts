import { TestBed } from '@angular/core/testing';

import { JamEventService } from './jam-event.service';

describe('AddEventService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: JamEventService = TestBed.get(JamEventService);
    expect(service).toBeTruthy();
  });
});
