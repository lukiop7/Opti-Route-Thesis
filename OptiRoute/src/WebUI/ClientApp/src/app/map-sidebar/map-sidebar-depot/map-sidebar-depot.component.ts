import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {FormGroup} from '@angular/forms';

@Component({
  selector: 'app-map-sidebar-depot',
  templateUrl: './map-sidebar-depot.component.html',
  styleUrls: ['./map-sidebar-depot.component.scss']
})
export class MapSidebarDepotComponent implements OnInit {
  @Input('group')
  public depotInfoForm: FormGroup;
  @Output() continueClicked = new EventEmitter<void>();
  public time: Date;
  constructor() { }

  ngOnInit(): void {
    this.time = new Date();
  }

  onClick() {
    this.continueClicked.emit();
  }
}
