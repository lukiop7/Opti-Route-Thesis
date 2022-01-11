import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MapSidebarGettingStartedComponent } from './map-sidebar-getting-started.component';

describe('MapSidebarGettingStartedComponent', () => {
  let component: MapSidebarGettingStartedComponent;
  let fixture: ComponentFixture<MapSidebarGettingStartedComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MapSidebarGettingStartedComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MapSidebarGettingStartedComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
