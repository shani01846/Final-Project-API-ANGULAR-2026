import { TestBed } from '@angular/core/testing';

import { LoteryResultService } from '../../lotery-result-service';

describe('LoteryResultService', () => {
  let service: LoteryResultService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(LoteryResultService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
