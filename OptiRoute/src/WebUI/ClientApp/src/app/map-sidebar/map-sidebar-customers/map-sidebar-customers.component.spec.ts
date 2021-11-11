import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MapSidebarCustomersComponent } from './map-sidebar-customers.component';

describe('MapSidebarCustomersComponent', () => {
  let component: MapSidebarCustomersComponent;
  let fixture: ComponentFixture<MapSidebarCustomersComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MapSidebarCustomersComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MapSidebarCustomersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
