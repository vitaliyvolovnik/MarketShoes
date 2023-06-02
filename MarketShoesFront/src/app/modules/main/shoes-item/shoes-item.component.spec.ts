import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ShoesItemComponent } from './shoes-item.component';

describe('ShoesItemComponent', () => {
  let component: ShoesItemComponent;
  let fixture: ComponentFixture<ShoesItemComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ShoesItemComponent]
    });
    fixture = TestBed.createComponent(ShoesItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
