import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MapSidebarDepotComponent } from './map-sidebar-depot.component';

describe('MapSidebarDepotComponent', () => {
  let component: MapSidebarDepotComponent;
  let fixture: ComponentFixture<MapSidebarDepotComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MapSidebarDepotComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MapSidebarDepotComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
