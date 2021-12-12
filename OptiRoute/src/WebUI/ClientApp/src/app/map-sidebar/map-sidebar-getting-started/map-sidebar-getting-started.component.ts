import {Component, EventEmitter, OnInit, Output} from '@angular/core';

@Component({
  selector: 'app-map-sidebar-getting-started',
  templateUrl: './map-sidebar-getting-started.component.html',
  styleUrls: ['./map-sidebar-getting-started.component.scss']
})
export class MapSidebarGettingStartedComponent implements OnInit {

  @Output() continueClicked = new EventEmitter<void>();

  constructor() { }

  ngOnInit(): void {
  }

  onClick() {
    this.continueClicked.emit();
  }
}
