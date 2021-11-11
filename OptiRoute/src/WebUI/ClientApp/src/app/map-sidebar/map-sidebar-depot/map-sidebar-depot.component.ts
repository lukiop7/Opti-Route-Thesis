import {Component, EventEmitter, OnInit, Output} from '@angular/core';

@Component({
  selector: 'app-map-sidebar-depot',
  templateUrl: './map-sidebar-depot.component.html',
  styleUrls: ['./map-sidebar-depot.component.scss']
})
export class MapSidebarDepotComponent implements OnInit {

  @Output() continueClicked = new EventEmitter<void>();

  constructor() { }

  ngOnInit(): void {
  }

  onClick() {
    this.continueClicked.emit();
  }
}
