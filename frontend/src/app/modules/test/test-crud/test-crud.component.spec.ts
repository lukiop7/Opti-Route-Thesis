import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TestCRUDComponent } from './test-crud.component';

describe('TestCRUDComponent', () => {
  let component: TestCRUDComponent;
  let fixture: ComponentFixture<TestCRUDComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TestCRUDComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TestCRUDComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
