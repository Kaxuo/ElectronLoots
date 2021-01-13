import { TestBed } from '@angular/core/testing';

import { PlayersFloorsService } from './players-floors.service';

describe('PlayersFloorsService', () => {
  let service: PlayersFloorsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PlayersFloorsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
