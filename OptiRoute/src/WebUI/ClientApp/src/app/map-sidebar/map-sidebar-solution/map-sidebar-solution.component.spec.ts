import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MapSidebarSolutionComponent } from './map-sidebar-solution.component';

describe('MapSidebarSolutionComponent', () => {
  let component: MapSidebarSolutionComponent;
  let fixture: ComponentFixture<MapSidebarSolutionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MapSidebarSolutionComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MapSidebarSolutionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
