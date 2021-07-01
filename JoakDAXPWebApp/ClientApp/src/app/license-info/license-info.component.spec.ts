import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LicenseInfoComponent } from './license-info.component';

describe('LicenseInfoComponent', () => {
  let component: LicenseInfoComponent;
  let fixture: ComponentFixture<LicenseInfoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LicenseInfoComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(LicenseInfoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
