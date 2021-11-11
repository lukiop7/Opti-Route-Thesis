import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MapSidebarProblemInfoComponent } from './map-sidebar-problem-info.component';

describe('MapSidebarProblemInfoComponent', () => {
  let component: MapSidebarProblemInfoComponent;
  let fixture: ComponentFixture<MapSidebarProblemInfoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MapSidebarProblemInfoComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MapSidebarProblemInfoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
