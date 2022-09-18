import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ForeignAccountInfoComponent } from './foreign-account-info.component';

describe('ForeignAccountInfoComponent', () => {
  let component: ForeignAccountInfoComponent;
  let fixture: ComponentFixture<ForeignAccountInfoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ForeignAccountInfoComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ForeignAccountInfoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
