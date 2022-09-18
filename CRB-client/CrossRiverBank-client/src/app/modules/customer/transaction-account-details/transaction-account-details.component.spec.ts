import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TransactionAccountDetailsComponent } from './transaction-account-details.component';

describe('TransactionAccountDetailsComponent', () => {
  let component: TransactionAccountDetailsComponent;
  let fixture: ComponentFixture<TransactionAccountDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TransactionAccountDetailsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TransactionAccountDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
