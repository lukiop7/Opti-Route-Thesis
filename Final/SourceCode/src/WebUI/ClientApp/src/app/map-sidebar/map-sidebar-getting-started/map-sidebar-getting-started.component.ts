import {Component, EventEmitter, OnDestroy, OnInit, Output} from '@angular/core';

@Component({
  selector: 'app-map-sidebar-getting-started',
  templateUrl: './map-sidebar-getting-started.component.html',
  styleUrls: ['./map-sidebar-getting-started.component.scss']
})
export class MapSidebarGettingStartedComponent implements OnInit, OnDestroy {

  @Output() continueClicked = new EventEmitter<void>();

  constructor() { }
  ngOnDestroy(): void {
  }

  ngOnInit(): void {
  }

  onClick() {
    this.continueClicked.emit();
  }
}
