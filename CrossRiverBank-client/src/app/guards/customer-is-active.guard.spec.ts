import { TestBed } from '@angular/core/testing';
import { CustomerIsActiveGuard } from './customer-is-active.guard';

describe('CustomerIsActiveGuard', () => {
  let guard: CustomerIsActiveGuard;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    guard = TestBed.inject(CustomerIsActiveGuard);
  });

  it('should be created', () => {
    expect(guard).toBeTruthy();
  });
});
