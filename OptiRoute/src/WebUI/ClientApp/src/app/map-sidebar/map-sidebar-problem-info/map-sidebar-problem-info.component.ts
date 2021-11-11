import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {FormGroup} from '@angular/forms';

@Component({
  selector: 'app-map-sidebar-problem-info',
  templateUrl: './map-sidebar-problem-info.component.html',
  styleUrls: ['./map-sidebar-problem-info.component.scss']
})
export class MapSidebarProblemInfoComponent implements OnInit {
  @Input('group')
  public problemInfoForm: FormGroup;
  @Output() continueClicked = new EventEmitter<void>();

  constructor() { }

  ngOnInit(): void {
  }

  onClick() {
    this.continueClicked.emit();
  }

}
