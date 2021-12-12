import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {FormGroup} from '@angular/forms';
import {localIsoTime} from '../../../shared/utils/localIsoTime';

@Component({
  selector: 'app-map-sidebar-depot',
  templateUrl: './map-sidebar-depot.component.html',
  styleUrls: ['./map-sidebar-depot.component.scss']
})
export class MapSidebarDepotComponent implements OnInit {
  @Input('group')
  public depotInfoForm: FormGroup;
  @Output() continueClicked = new EventEmitter<void>();
  constructor() {
  }

  ngOnInit(): void {
    this.depotInfoForm.patchValue({
      dueDate: (localIsoTime().slice(0, 16))
    });
  }

  onClick() {
    this.continueClicked.emit();
  }


}

