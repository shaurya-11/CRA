import { TestBed } from '@angular/core/testing';

import { PatchDetailsService } from './patch-details';

describe('PatchDetails', () => {
  let service: PatchDetailsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PatchDetailsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
