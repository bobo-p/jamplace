import { TestBed } from '@angular/core/testing';

import { JamUserService } from './jam-user.service';

describe('JamUserService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: JamUserService = TestBed.get(JamUserService);
    expect(service).toBeTruthy();
  });
});
