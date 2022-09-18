import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UpdataBalanceComponent } from './updata-balance.component';

describe('UpdataBalanceComponent', () => {
  let component: UpdataBalanceComponent;
  let fixture: ComponentFixture<UpdataBalanceComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UpdataBalanceComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UpdataBalanceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
